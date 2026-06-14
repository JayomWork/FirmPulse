namespace FirmPulse.Entities;

public static class WorkItemClientDocumentStatuses
{
    public const string NotRequired = "Not Required";
    public const string Pending = "Pending";
    public const string PartiallyReceived = "Partially Received";
    public const string Received = "Received";
    public const string Reviewed = "Reviewed";

    public static readonly string[] All = [NotRequired, Pending, PartiallyReceived, Received, Reviewed];
}
