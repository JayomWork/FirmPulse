namespace FirmPulse.Entities;

public static class DueDateBasedOnTypes
{
    public const string FinancialYearEnd = "FinancialYearEnd";
    public const string AGMDate = "AGMDate";
    public const string IncorporationDate = "IncorporationDate";
    public const string EventDate = "EventDate";
    public const string Manual = "Manual";

    public static readonly string[] All = [FinancialYearEnd, AGMDate, IncorporationDate, EventDate, Manual];
}
