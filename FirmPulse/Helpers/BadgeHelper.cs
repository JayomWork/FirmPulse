using FirmPulse.Entities;

namespace FirmPulse.Helpers;

public static class BadgeHelper
{
    public static string GetTaskStatusBadge(string status) => status switch
    {
        ComplianceTaskStatuses.Completed or ComplianceTaskStatuses.Filed => "bg-success-subtle text-success-emphasis",
        ComplianceTaskStatuses.Overdue => "bg-danger-subtle text-danger-emphasis",
        ComplianceTaskStatuses.InProgress => "bg-primary-subtle text-primary-emphasis",
        ComplianceTaskStatuses.WaitingClient => "bg-warning-subtle text-warning-emphasis",
        _ => "bg-secondary-subtle text-secondary-emphasis"
    };

    public static string GetPriorityBadge(string priority) => priority switch
    {
        ComplianceTaskPriorities.Critical => "bg-danger text-white",
        ComplianceTaskPriorities.High => "bg-warning text-dark",
        ComplianceTaskPriorities.Medium => "bg-info text-dark",
        _ => "bg-light text-dark border"
    };

    public static string GetInvoiceStatusBadge(string status) => status switch
    {
        InvoiceStatuses.Paid => "bg-success-subtle text-success-emphasis",
        InvoiceStatuses.PartiallyPaid => "bg-info-subtle text-info-emphasis",
        InvoiceStatuses.Overdue => "bg-danger-subtle text-danger-emphasis",
        InvoiceStatuses.Cancelled => "bg-secondary-subtle text-secondary-emphasis",
        InvoiceStatuses.Sent => "bg-primary-subtle text-primary-emphasis",
        _ => "bg-warning-subtle text-warning-emphasis"
    };
}
