namespace FirmPulse.Entities;

public static class WorkItemFilingStatuses
{
    public const string NotRequired = "Not Required";
    public const string Pending = "Pending";
    public const string Filed = "Filed";
    public const string Approved = "Approved";
    public const string ResubmissionRequired = "Resubmission Required";

    public static readonly string[] All = [NotRequired, Pending, Filed, Approved, ResubmissionRequired];
}
