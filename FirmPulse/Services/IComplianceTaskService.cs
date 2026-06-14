using FirmPulse.Entities;

namespace FirmPulse.Services;

public interface IComplianceTaskService
{
    Task<List<ComplianceTask>> GetAllAsync();
    Task<ComplianceTask?> GetByIdAsync(int id);
    Task<List<ComplianceTask>> GetByCompanyIdAsync(int companyClientId);
    Task<List<ComplianceTask>> GetUpcomingAsync();
    Task<List<ComplianceTask>> GetOverdueAsync();
    Task<ComplianceTask> CreateAsync(ComplianceTask entity);
    Task UpdateAsync(ComplianceTask entity);
    Task DeleteAsync(int id);
    Task MarkCompletedAsync(int id);
}
