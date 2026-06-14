using FirmPulse.Entities;

namespace FirmPulse.Services;

public interface IComplianceTaskService
{
    Task<List<ComplianceTask>> GetAllAsync();
    Task<List<ComplianceTask>> GetByCompanyIdAsync(int companyClientId);
    Task<List<ComplianceTask>> GetUpcomingAsync();
    Task<List<ComplianceTask>> GetOverdueAsync();
    Task<ComplianceTask> CreateAsync(ComplianceTask entity);
    Task UpdateAsync(ComplianceTask entity);
    Task MarkCompletedAsync(int id);
}
