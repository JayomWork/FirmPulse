using FirmPulse.Entities;

namespace FirmPulse.Services;

public interface IDocumentTemplateService
{
    Task<List<DocumentTemplate>> GetTemplatesAsync(bool includeInactive = false);
    Task<DocumentTemplate?> GetTemplateByIdAsync(int id);
    Task<DocumentTemplate> CreateTemplateAsync(DocumentTemplate template);
    Task UpdateTemplateAsync(DocumentTemplate template);
    Task DeleteTemplateAsync(int id);
    IReadOnlyList<string> ExtractPlaceholders(string content);
}
