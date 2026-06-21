using FirmPulse.Entities;
using FirmPulse.Models;

namespace FirmPulse.Services;

public interface IDocumentGenerationService
{
    Task<List<DocumentGenerationFieldModel>> GetGenerationFieldsAsync(int companyClientId, int documentTemplateId, int? directorId = null);
    Task<string> PreviewDocumentAsync(int documentTemplateId, IEnumerable<DocumentGenerationFieldModel> fields);
    Task<GeneratedDocument> GenerateDocumentAsync(int companyClientId, int documentTemplateId, string documentTitle, IEnumerable<DocumentGenerationFieldModel> fields, string? generatedBy = null);
    Task<string> GenerateWordAsync(GeneratedDocument document);
    Task<List<GeneratedDocument>> GetGeneratedDocumentsAsync();
    Task<GeneratedDocument?> GetGeneratedDocumentByIdAsync(int id);
}
