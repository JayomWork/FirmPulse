namespace FirmPulse.Entities;

public static class WorkItemDocumentStatuses
{
    public const string NotRequired = "Not Required";
    public const string PendingFromClient = "Pending From Client";
    public const string Received = "Received";
    public const string Reviewed = "Reviewed";
    public const string Uploaded = "Uploaded";
    public const string Rejected = "Rejected";

    public static readonly string[] All = [NotRequired, PendingFromClient, Received, Reviewed, Uploaded, Rejected];
}
