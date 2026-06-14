namespace FirmPulse.Models;

public class DashboardSummaryModel
{
    public int TotalCompanies { get; set; }
    public int PendingTasks { get; set; }
    public int OverdueTasks { get; set; }
    public int UpcomingTasks { get; set; }
    public decimal UnpaidInvoiceTotal { get; set; }
    public decimal PaidInvoiceTotal { get; set; }
}
