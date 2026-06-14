namespace FirmPulse.Entities;

public static class ComplianceTaskStatuses
{
    public const string Pending = "Pending";
    public const string InProgress = "In Progress";
    public const string WaitingClient = "Waiting Client";
    public const string Filed = "Filed";
    public const string Completed = "Completed";
    public const string Overdue = "Overdue";

    public static readonly string[] All =
    [
        Pending,
        InProgress,
        WaitingClient,
        Filed,
        Completed,
        Overdue
    ];
}
