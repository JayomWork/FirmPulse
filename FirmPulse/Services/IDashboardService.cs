using FirmPulse.Entities;
using FirmPulse.Models;

namespace FirmPulse.Services;

public interface IDashboardService
{
    Task<DashboardSummaryModel> GetSummaryAsync();
    Task<List<ComplianceTask>> GetUpcomingTasksAsync();
}
