using FirmPulse.Entities;
using FirmPulse.Models;

namespace FirmPulse.Services;

public interface IDashboardService
{
    Task<DashboardSummaryModel> GetSummaryAsync();
    Task<List<ComplianceTask>> GetUpcomingTasksAsync();
    Task<CsDashboardSummaryModel> GetCsDashboardSummaryAsync();
    Task<List<WorkItem>> GetUpcomingWorkItemsAsync();
    Task<List<WorkItem>> GetOverdueWorkItemsAsync();
    Task<List<WorkItem>> GetPendingDocumentWorkItemsAsync();
    Task<List<CompanyMeeting>> GetUpcomingMeetingsAsync();
}
