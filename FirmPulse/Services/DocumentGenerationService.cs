using System.IO.Compression;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using FirmPulse.Data;
using FirmPulse.Entities;
using FirmPulse.Models;
using Microsoft.EntityFrameworkCore;

namespace FirmPulse.Services;

public partial class DocumentGenerationService(
    FirmPulseDbContext context,
    IDocumentTemplateService templateService,
    IWebHostEnvironment environment) : IDocumentGenerationService
{
    public async Task<List<DocumentGenerationFieldModel>> GetGenerationFieldsAsync(int companyClientId, int documentTemplateId, int? directorId = null)
    {
        var template = await context.DocumentTemplates
            .Include(x => x.Fields.OrderBy(f => f.DisplayOrder))
            .FirstOrDefaultAsync(x => x.Id == documentTemplateId);

        if (template is null)
        {
            return [];
        }

        var company = await context.CompanyClients
            .Include(x => x.Directors.Where(d => d.IsActive))
            .FirstOrDefaultAsync(x => x.Id == companyClientId);

        var selectedDirector = directorId.HasValue
            ? company?.Directors.FirstOrDefault(x => x.Id == directorId.Value)
            : company?.Directors.OrderBy(x => x.FullName).FirstOrDefault();

        var knownValues = BuildKnownValues(company, selectedDirector);
        var fieldDefinitions = template.Fields.Count != 0
            ? template.Fields.OrderBy(x => x.DisplayOrder).ToList()
            : templateService.ExtractPlaceholders(template.TemplateContent)
                .Select((key, index) => new DocumentTemplateField
                {
                    FieldKey = key,
                    FieldLabel = ToLabel(key),
                    FieldType = key.Contains("Date", StringComparison.OrdinalIgnoreCase) ? "Date" : "Text",
                    IsRequired = true,
                    DisplayOrder = index + 1
                })
                .ToList();

        return fieldDefinitions
            .Select(field =>
            {
                knownValues.TryGetValue(field.FieldKey, out var value);
                return new DocumentGenerationFieldModel
                {
                    FieldKey = field.FieldKey,
                    FieldLabel = field.FieldLabel,
                    FieldType = field.FieldType,
                    DataSource = field.DataSource,
                    IsRequired = field.IsRequired,
                    DisplayOrder = field.DisplayOrder,
                    DefaultValue = field.DefaultValue,
                    FieldValue = string.IsNullOrWhiteSpace(value) ? field.DefaultValue : value,
                    IsAutoFilled = !string.IsNullOrWhiteSpace(value)
                };
            })
            .OrderBy(x => x.DisplayOrder)
            .ToList();
    }

    public async Task<string> PreviewDocumentAsync(int documentTemplateId, IEnumerable<DocumentGenerationFieldModel> fields)
    {
        var template = await context.DocumentTemplates.FirstOrDefaultAsync(x => x.Id == documentTemplateId);
        return template is null ? string.Empty : ReplacePlaceholders(template.TemplateContent, fields);
    }

    public async Task<GeneratedDocument> GenerateDocumentAsync(int companyClientId, int documentTemplateId, string documentTitle, IEnumerable<DocumentGenerationFieldModel> fields, string? generatedBy = null)
    {
        var fieldList = fields.ToList();
        var content = await PreviewDocumentAsync(documentTemplateId, fieldList);

        var document = new GeneratedDocument
        {
            CompanyClientId = companyClientId,
            DocumentTemplateId = documentTemplateId,
            DocumentTitle = documentTitle,
            GeneratedContent = content,
            GeneratedBy = string.IsNullOrWhiteSpace(generatedBy) ? "System" : generatedBy,
            GeneratedAt = DateTime.UtcNow,
            Values = fieldList.Select(field => new GeneratedDocumentValue
            {
                FieldKey = field.FieldKey,
                FieldValue = field.FieldValue
            }).ToList()
        };

        context.GeneratedDocuments.Add(document);
        await context.SaveChangesAsync();
        await GenerateWordAsync(document);

        return document;
    }

    public async Task<string> GenerateWordAsync(GeneratedDocument document)
    {
        var webRoot = environment.WebRootPath;
        if (string.IsNullOrWhiteSpace(webRoot))
        {
            webRoot = Path.Combine(AppContext.BaseDirectory, "wwwroot");
        }

        var relativeDirectory = Path.Combine("generated-documents", document.CompanyClientId.ToString(), document.Id.ToString());
        var absoluteDirectory = Path.Combine(webRoot, relativeDirectory);
        Directory.CreateDirectory(absoluteDirectory);

        var fileName = $"{SanitizeFileName(document.DocumentTitle)}.docx";
        var absolutePath = Path.Combine(absoluteDirectory, fileName);
        CreateDocx(absolutePath, document.GeneratedContent);

        var relativePath = Path.Combine(relativeDirectory, fileName).Replace('\\', '/');
        document.WordFilePath = relativePath;
        await context.SaveChangesAsync();

        return relativePath;
    }

    public async Task<List<GeneratedDocument>> GetGeneratedDocumentsAsync()
    {
        return await context.GeneratedDocuments
            .Include(x => x.CompanyClient)
            .Include(x => x.DocumentTemplate)
            .Include(x => x.Values)
            .OrderByDescending(x => x.GeneratedAt)
            .ToListAsync();
    }

    public async Task<GeneratedDocument?> GetGeneratedDocumentByIdAsync(int id)
    {
        return await context.GeneratedDocuments
            .Include(x => x.CompanyClient)
            .Include(x => x.DocumentTemplate)
            .Include(x => x.Values)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    private static string ReplacePlaceholders(string content, IEnumerable<DocumentGenerationFieldModel> fields)
    {
        var values = fields.ToDictionary(x => x.FieldKey, x => x.FieldValue ?? string.Empty, StringComparer.OrdinalIgnoreCase);

        return PlaceholderRegex().Replace(content, match =>
        {
            var key = match.Groups["key"].Value.Trim();
            return values.TryGetValue(key, out var value) ? value : string.Empty;
        });
    }

    private static Dictionary<string, string?> BuildKnownValues(CompanyClient? company, Director? director)
    {
        var values = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
        if (company is not null)
        {
            values["CompanyName"] = company.CompanyName;
            values["CIN"] = company.CIN;
            values["RegistrationNumber"] = company.RegistrationNumber;
            values["CompanyType"] = company.CompanyType;
            values["RegisteredOfficeAddress"] = company.RegisteredOfficeAddress;
            values["City"] = company.City;
            values["State"] = company.State;
            values["PinCode"] = company.PinCode;
            values["Email"] = company.Email;
            values["Phone"] = company.Phone;
            values["AuthorizedCapital"] = company.AuthorizedCapital?.ToString("0.##");
            values["PaidUpCapital"] = company.PaidUpCapital?.ToString("0.##");
            values["FinancialYearStart"] = FormatDate(company.FinancialYearStart);
            values["FinancialYearEnd"] = FormatDate(company.FinancialYearEnd);
        }

        if (director is not null)
        {
            values["DirectorName"] = director.FullName;
            values["DIN"] = director.DIN;
            values["DirectorPAN"] = director.PAN;
            values["DirectorEmail"] = director.Email;
            values["DirectorPhone"] = director.Phone;
            values["DirectorAddress"] = director.Address;
            values["Designation"] = director.Designation;
            values["AppointmentDate"] = FormatDate(director.AppointmentDate);
            values["ResignationDate"] = FormatDate(director.ResignationDate);
        }

        return values;
    }

    private static void CreateDocx(string absolutePath, string content)
    {
        if (File.Exists(absolutePath))
        {
            File.Delete(absolutePath);
        }

        using var archive = ZipFile.Open(absolutePath, ZipArchiveMode.Create);
        WriteEntry(archive, "[Content_Types].xml", """
            <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
            <Types xmlns="http://schemas.openxmlformats.org/package/2006/content-types">
              <Default Extension="rels" ContentType="application/vnd.openxmlformats-package.relationships+xml"/>
              <Default Extension="xml" ContentType="application/xml"/>
              <Override PartName="/word/document.xml" ContentType="application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml"/>
            </Types>
            """);

        WriteEntry(archive, "_rels/.rels", """
            <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
            <Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">
              <Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument" Target="word/document.xml"/>
            </Relationships>
            """);

        WriteEntry(archive, "word/document.xml", BuildDocumentXml(content));
    }

    private static string BuildDocumentXml(string content)
    {
        var paragraphs = content.Replace("\r\n", "\n").Split('\n');
        var body = new StringBuilder();
        foreach (var paragraph in paragraphs)
        {
            body.Append("<w:p><w:r><w:t xml:space=\"preserve\">");
            body.Append(SecurityElement.Escape(paragraph));
            body.Append("</w:t></w:r></w:p>");
        }

        return $$"""
            <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
            <w:document xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
              <w:body>
                {{body}}
                <w:sectPr><w:pgSz w:w="11906" w:h="16838"/><w:pgMar w:top="1440" w:right="1440" w:bottom="1440" w:left="1440"/></w:sectPr>
              </w:body>
            </w:document>
            """;
    }

    private static void WriteEntry(ZipArchive archive, string entryName, string content)
    {
        var entry = archive.CreateEntry(entryName);
        using var stream = entry.Open();
        using var writer = new StreamWriter(stream, Encoding.UTF8);
        writer.Write(content);
    }

    private static string SanitizeFileName(string title)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        var sanitized = new string(title.Select(ch => invalidChars.Contains(ch) ? '-' : ch).ToArray());
        return string.IsNullOrWhiteSpace(sanitized) ? "generated-document" : sanitized.Trim();
    }

    private static string ToLabel(string key)
    {
        var spaced = Regex.Replace(key, "([a-z])([A-Z])", "$1 $2");
        return spaced.Replace("_", " ");
    }

    private static string? FormatDate(DateOnly? value) => value?.ToString("dd MMM yyyy");

    [GeneratedRegex(@"\{\{\s*(?<key>[A-Za-z0-9_]+)\s*\}\}", RegexOptions.Compiled)]
    private static partial Regex PlaceholderRegex();
}
