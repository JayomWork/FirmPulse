namespace FirmPulse.Entities;

public static class InvoiceStatuses
{
    public const string Draft = "Draft";
    public const string Sent = "Sent";
    public const string Paid = "Paid";
    public const string PartiallyPaid = "Partially Paid";
    public const string Cancelled = "Cancelled";
    public const string Overdue = "Overdue";

    public static readonly string[] All =
    [
        Draft,
        Sent,
        Paid,
        PartiallyPaid,
        Cancelled,
        Overdue
    ];
}
