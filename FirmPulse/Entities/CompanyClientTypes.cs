namespace FirmPulse.Entities;

public static class CompanyClientTypes
{
    public const string PrivateLimited = "Private Limited";
    public const string PublicLimited = "Public Limited";
    public const string Llp = "LLP";
    public const string Opc = "OPC";
    public const string Section8 = "Section 8 Company";
    public const string Partnership = "Partnership Firm";

    public static readonly string[] All =
    [
        PrivateLimited,
        PublicLimited,
        Llp,
        Opc,
        Section8,
        Partnership
    ];
}
