using FirmPulse.Entities;

namespace FirmPulse.Helpers;

public static class WorkItemHelper
{
    public static bool IsOverdue(WorkItem workItem)
    {
        return workItem.DueDate.HasValue &&
               workItem.DueDate.Value < DateOnly.FromDateTime(DateTime.Today) &&
               workItem.Status is not WorkItemStatuses.Filed and not WorkItemStatuses.Completed and not WorkItemStatuses.Cancelled;
    }

    public static string GetDisplayStatus(WorkItem workItem)
    {
        return IsOverdue(workItem) ? WorkItemStatuses.Overdue : workItem.Status;
    }

    public static string GetNextBestAction(WorkItem workItem)
    {
        if (workItem.RequiresClientDocuments && workItem.ClientDocumentsStatus is WorkItemClientDocumentStatuses.Pending or WorkItemClientDocumentStatuses.PartiallyReceived)
        {
            return "Follow up with client";
        }

        if (workItem.RequiresMcaFiling &&
            workItem.ClientDocumentsStatus is WorkItemClientDocumentStatuses.Received or WorkItemClientDocumentStatuses.Reviewed &&
            workItem.FilingStatus == WorkItemFilingStatuses.Pending)
        {
            return "Prepare/Filing pending";
        }

        if (workItem.FilingStatus == WorkItemFilingStatuses.Filed && workItem.Status != WorkItemStatuses.Completed)
        {
            return "Update SRN/challan and complete";
        }

        return "Review work item";
    }
}
