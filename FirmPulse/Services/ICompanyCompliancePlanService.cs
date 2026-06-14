using FirmPulse.Entities;

namespace FirmPulse.Services;

public interface ICompanyCompliancePlanService
{
    Task<List<CompanyCompliancePlan>> GetPlansAsync();
    Task<List<CompanyCompliancePlan>> GetByCompanyIdAsync(int companyClientId);
    Task<CompanyCompliancePlan?> GetByIdAsync(int id);
    Task<CompanyCompliancePlan> CreatePlanAsync(CompanyCompliancePlan plan);
    Task GenerateWorkItemsFromTemplateAsync(int planId);
    Task MarkPlanCompletedAsync(int planId);
}
