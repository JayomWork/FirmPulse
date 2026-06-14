using FirmPulse.Entities;

namespace FirmPulse.Helpers;

public static class ComplianceTaskHelper
{
    public static bool IsOverdue(ComplianceTask task)
    {
        return task.DueDate < DateOnly.FromDateTime(DateTime.Today) &&
               task.Status is not ComplianceTaskStatuses.Completed and not ComplianceTaskStatuses.Filed;
    }

    public static string GetDisplayStatus(ComplianceTask task)
    {
        return IsOverdue(task) ? ComplianceTaskStatuses.Overdue : task.Status;
    }
}
