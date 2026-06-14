namespace FirmPulse.Entities;

public static class CompanyMeetingTypes
{
    public const string BoardMeeting = "Board Meeting";
    public const string AGM = "AGM";
    public const string EGM = "EGM";
    public const string CommitteeMeeting = "Committee Meeting";

    public static readonly string[] All = [BoardMeeting, AGM, EGM, CommitteeMeeting];
}
