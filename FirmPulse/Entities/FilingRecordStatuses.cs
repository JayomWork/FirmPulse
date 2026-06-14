namespace FirmPulse.Entities;

public static class FilingRecordStatuses
{
    public const string NotStarted = "Not Started";
    public const string Prepared = "Prepared";
    public const string Uploaded = "Uploaded";
    public const string Filed = "Filed";
    public const string Approved = "Approved";
    public const string Rejected = "Rejected";
    public const string ResubmissionRequired = "Resubmission Required";

    public static readonly string[] All = [NotStarted, Prepared, Uploaded, Filed, Approved, Rejected, ResubmissionRequired];
}
