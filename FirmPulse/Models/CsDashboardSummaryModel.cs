namespace FirmPulse.Models;

public class CsDashboardSummaryModel
{
    public int TotalActiveCompanies { get; set; }
    public int DueThisWeekCount { get; set; }
    public int DueThisMonthCount { get; set; }
    public int OverdueWorkItemsCount { get; set; }
    public int PendingClientDocumentsCount { get; set; }
    public int PendingMcaFilingsCount { get; set; }
    public int FiledThisMonthCount { get; set; }
    public int UpcomingAgmCount { get; set; }
    public int UpcomingBoardMeetingCount { get; set; }
    public decimal UnpaidInvoiceTotal { get; set; }
}
