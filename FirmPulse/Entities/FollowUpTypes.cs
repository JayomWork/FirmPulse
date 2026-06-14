namespace FirmPulse.Entities;

public static class FollowUpTypes
{
    public const string Call = "Call";
    public const string WhatsApp = "WhatsApp";
    public const string Email = "Email";
    public const string Meeting = "Meeting";
    public const string InternalNote = "Internal Note";

    public static readonly string[] All = [Call, WhatsApp, Email, Meeting, InternalNote];
}
