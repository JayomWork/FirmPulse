namespace FirmPulse.Entities;

public static class CompanyClientStatuses
{
    public const string Active = "Active";
    public const string OnHold = "On Hold";
    public const string Dormant = "Dormant";
    public const string Closed = "Closed";

    public static readonly string[] All =
    [
        Active,
        OnHold,
        Dormant,
        Closed
    ];
}
