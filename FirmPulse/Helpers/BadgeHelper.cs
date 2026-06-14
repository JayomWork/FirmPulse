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

    public static string GetWorkItemStatusBadge(string status) => status switch
    {
        WorkItemStatuses.Completed or WorkItemStatuses.Filed => "bg-success-subtle text-success-emphasis",
        WorkItemStatuses.Overdue => "bg-danger-subtle text-danger-emphasis",
        WorkItemStatuses.InProgress => "bg-primary-subtle text-primary-emphasis",
        WorkItemStatuses.WaitingClient => "bg-warning-subtle text-warning-emphasis",
        WorkItemStatuses.ReadyForFiling => "bg-info-subtle text-info-emphasis",
        WorkItemStatuses.Cancelled => "bg-secondary-subtle text-secondary-emphasis",
        _ => "bg-secondary-subtle text-secondary-emphasis"
    };

    public static string GetWorkItemDocumentBadge(string status) => status switch
    {
        WorkItemDocumentStatuses.Uploaded or WorkItemDocumentStatuses.Reviewed => "bg-success-subtle text-success-emphasis",
        WorkItemDocumentStatuses.Received => "bg-info-subtle text-info-emphasis",
        WorkItemDocumentStatuses.Rejected => "bg-danger-subtle text-danger-emphasis",
        WorkItemDocumentStatuses.PendingFromClient => "bg-warning-subtle text-warning-emphasis",
        _ => "bg-secondary-subtle text-secondary-emphasis"
    };

    public static string GetFilingStatusBadge(string status) => status switch
    {
        FilingRecordStatuses.Approved or FilingRecordStatuses.Filed => "bg-success-subtle text-success-emphasis",
        FilingRecordStatuses.Prepared or FilingRecordStatuses.Uploaded => "bg-info-subtle text-info-emphasis",
        FilingRecordStatuses.Rejected or FilingRecordStatuses.ResubmissionRequired => "bg-danger-subtle text-danger-emphasis",
        _ => "bg-secondary-subtle text-secondary-emphasis"
    };

    public static string GetMeetingStatusBadge(string status) => status switch
    {
        CompanyMeetingStatuses.Completed => "bg-success-subtle text-success-emphasis",
        CompanyMeetingStatuses.Cancelled => "bg-secondary-subtle text-secondary-emphasis",
        CompanyMeetingStatuses.Held or CompanyMeetingStatuses.MinutesPrepared => "bg-info-subtle text-info-emphasis",
        CompanyMeetingStatuses.NoticePrepared => "bg-primary-subtle text-primary-emphasis",
        _ => "bg-warning-subtle text-warning-emphasis"
    };

    public static string GetFollowUpStatusBadge(string status) => status switch
    {
        ClientFollowUpStatuses.Done => "bg-success-subtle text-success-emphasis",
        ClientFollowUpStatuses.NoResponse => "bg-danger-subtle text-danger-emphasis",
        ClientFollowUpStatuses.WaitingClient => "bg-warning-subtle text-warning-emphasis",
        _ => "bg-secondary-subtle text-secondary-emphasis"
    };
}
