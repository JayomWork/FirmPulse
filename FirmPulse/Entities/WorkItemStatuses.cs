namespace FirmPulse.Entities;

public static class WorkItemStatuses
{
    public const string NotStarted = "Not Started";
    public const string InProgress = "In Progress";
    public const string WaitingClient = "Waiting Client";
    public const string ReadyForFiling = "Ready For Filing";
    public const string Filed = "Filed";
    public const string Completed = "Completed";
    public const string Cancelled = "Cancelled";
    public const string Overdue = "Overdue";

    public static readonly string[] All = [NotStarted, InProgress, WaitingClient, ReadyForFiling, Filed, Completed, Cancelled, Overdue];
}
