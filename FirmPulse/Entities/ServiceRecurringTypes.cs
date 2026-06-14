namespace FirmPulse.Entities;

public static class ServiceRecurringTypes
{
    public const string Yearly = "Yearly";
    public const string HalfYearly = "HalfYearly";
    public const string Quarterly = "Quarterly";
    public const string Monthly = "Monthly";
    public const string EventBased = "EventBased";

    public static readonly string[] All = [Yearly, HalfYearly, Quarterly, Monthly, EventBased];
}
