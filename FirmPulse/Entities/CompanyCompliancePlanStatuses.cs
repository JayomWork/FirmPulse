namespace FirmPulse.Entities;

public static class CompanyCompliancePlanStatuses
{
    public const string Draft = "Draft";
    public const string Active = "Active";
    public const string InProgress = "In Progress";
    public const string Completed = "Completed";
    public const string Cancelled = "Cancelled";

    public static readonly string[] All = [Draft, Active, InProgress, Completed, Cancelled];
}
