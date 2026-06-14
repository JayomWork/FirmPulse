using FirmPulse.Entities;

namespace FirmPulse.Services;

public interface IComplianceTemplateService
{
    Task<List<ComplianceTemplate>> GetTemplatesAsync();
    Task<ComplianceTemplate?> GetTemplateByIdAsync(int id);
    Task<List<ComplianceTemplateItem>> GetTemplateItemsAsync(int templateId);
    Task<ComplianceTemplate> CreateTemplateAsync(ComplianceTemplate template);
    Task UpdateTemplateAsync(ComplianceTemplate template);
    Task<ComplianceTemplateItem> AddTemplateItemAsync(ComplianceTemplateItem item);
    Task UpdateTemplateItemAsync(ComplianceTemplateItem item);
}
