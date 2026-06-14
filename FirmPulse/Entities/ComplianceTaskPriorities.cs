namespace FirmPulse.Entities;

public static class ComplianceTaskPriorities
{
    public const string Low = "Low";
    public const string Medium = "Medium";
    public const string High = "High";
    public const string Critical = "Critical";

    public static readonly string[] All =
    [
        Low,
        Medium,
        High,
        Critical
    ];
}
