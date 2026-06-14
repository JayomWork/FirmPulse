using FirmPulse.Entities;

namespace FirmPulse.Services;

public interface IWorkItemService
{
    Task<List<WorkItem>> GetAllAsync();
    Task<WorkItem?> GetByIdAsync(int id);
    Task<List<WorkItem>> GetByCompanyIdAsync(int companyClientId);
    Task<List<WorkItem>> GetDueThisWeekAsync();
    Task<List<WorkItem>> GetDueThisMonthAsync();
    Task<List<WorkItem>> GetOverdueAsync();
    Task<List<WorkItem>> GetPendingClientDocumentsAsync();
    Task<WorkItem> CreateAsync(WorkItem entity);
    Task UpdateAsync(WorkItem entity);
    Task ChangeStatusAsync(int workItemId, string newStatus, string remarks);
    Task MarkWaitingClientAsync(int workItemId);
    Task MarkReadyForFilingAsync(int workItemId);
    Task MarkFiledAsync(int workItemId);
    Task MarkCompletedAsync(int workItemId);
}
