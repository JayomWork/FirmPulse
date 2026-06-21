using System.Text.RegularExpressions;
using FirmPulse.Data;
using FirmPulse.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirmPulse.Services;

public partial class DocumentTemplateService(FirmPulseDbContext context) : IDocumentTemplateService
{
    public async Task<List<DocumentTemplate>> GetTemplatesAsync(bool includeInactive = false)
    {
        var query = context.DocumentTemplates
            .Include(x => x.Fields.OrderBy(f => f.DisplayOrder))
            .AsQueryable();

        if (!includeInactive)
        {
            query = query.Where(x => x.IsActive);
        }

        return await query
            .OrderBy(x => x.TemplateCategory)
            .ThenBy(x => x.TemplateName)
            .ToListAsync();
    }

    public async Task<DocumentTemplate?> GetTemplateByIdAsync(int id)
    {
        return await context.DocumentTemplates
            .Include(x => x.Fields.OrderBy(f => f.DisplayOrder))
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<DocumentTemplate> CreateTemplateAsync(DocumentTemplate template)
    {
        template.CreatedAt = DateTime.UtcNow;
        template.UpdatedAt = DateTime.UtcNow;
        SyncFields(template);

        context.DocumentTemplates.Add(template);
        await context.SaveChangesAsync();
        return template;
    }

    public async Task UpdateTemplateAsync(DocumentTemplate template)
    {
        var existing = await context.DocumentTemplates
            .Include(x => x.Fields)
            .FirstOrDefaultAsync(x => x.Id == template.Id);

        if (existing is null)
        {
            return;
        }

        existing.TemplateName = template.TemplateName;
        existing.TemplateCategory = template.TemplateCategory;
        existing.Description = template.Description;
        existing.TemplateContent = template.TemplateContent;
        existing.IsActive = template.IsActive;
        existing.UpdatedAt = DateTime.UtcNow;

        context.DocumentTemplateFields.RemoveRange(existing.Fields);
        SyncFields(existing);

        await context.SaveChangesAsync();
    }

    public async Task DeleteTemplateAsync(int id)
    {
        var existing = await context.DocumentTemplates.FirstOrDefaultAsync(x => x.Id == id);
        if (existing is null)
        {
            return;
        }

        existing.IsActive = false;
        existing.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }

    public IReadOnlyList<string> ExtractPlaceholders(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return [];
        }

        return PlaceholderRegex()
            .Matches(content)
            .Select(match => match.Groups["key"].Value.Trim())
            .Where(key => !string.IsNullOrWhiteSpace(key))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(key => key)
            .ToList();
    }

    private void SyncFields(DocumentTemplate template)
    {
        template.Fields.Clear();

        var placeholders = ExtractPlaceholders(template.TemplateContent);
        for (var index = 0; index < placeholders.Count; index++)
        {
            var key = placeholders[index];
            template.Fields.Add(new DocumentTemplateField
            {
                FieldKey = key,
                FieldLabel = ToLabel(key),
                FieldType = GetFieldType(key),
                DataSource = GetDataSource(key),
                IsRequired = true,
                DisplayOrder = index + 1
            });
        }
    }

    private static string ToLabel(string key)
    {
        var spaced = Regex.Replace(key, "([a-z])([A-Z])", "$1 $2");
        return spaced.Replace("_", " ");
    }

    private static string GetFieldType(string key)
    {
        return key.Contains("Date", StringComparison.OrdinalIgnoreCase) ||
            key.Equals("FinancialYearStart", StringComparison.OrdinalIgnoreCase) ||
            key.Equals("FinancialYearEnd", StringComparison.OrdinalIgnoreCase)
                ? "Date"
                : "Text";
    }

    private static string? GetDataSource(string key)
    {
        if (CompanyFieldKeys.Contains(key, StringComparer.OrdinalIgnoreCase))
        {
            return "Company";
        }

        if (DirectorFieldKeys.Contains(key, StringComparer.OrdinalIgnoreCase))
        {
            return "Director";
        }

        return null;
    }

    private static readonly string[] CompanyFieldKeys =
    [
        "CompanyName", "CIN", "RegistrationNumber", "CompanyType", "RegisteredOfficeAddress",
        "City", "State", "PinCode", "Email", "Phone", "AuthorizedCapital", "PaidUpCapital",
        "FinancialYearStart", "FinancialYearEnd"
    ];

    private static readonly string[] DirectorFieldKeys =
    [
        "DirectorName", "DIN", "DirectorPAN", "DirectorEmail", "DirectorPhone", "DirectorAddress",
        "Designation", "AppointmentDate", "ResignationDate"
    ];

    [GeneratedRegex(@"\{\{\s*(?<key>[A-Za-z0-9_]+)\s*\}\}", RegexOptions.Compiled)]
    private static partial Regex PlaceholderRegex();
}
