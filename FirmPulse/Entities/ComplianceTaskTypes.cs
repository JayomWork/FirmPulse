namespace FirmPulse.Entities;

public static class ComplianceTaskTypes
{
    public const string AnnualFiling = "Annual Filing";
    public const string BoardMeeting = "Board Meeting";
    public const string McaFiling = "MCA Filing";
    public const string RocCompliance = "ROC Compliance";
    public const string Agm = "AGM";
    public const string StatutoryRegister = "Statutory Register";

    public static readonly string[] All =
    [
        AnnualFiling,
        BoardMeeting,
        McaFiling,
        RocCompliance,
        Agm,
        StatutoryRegister
    ];
}
