namespace FirmPulse.Entities;

public static class CompanyMeetingStatuses
{
    public const string Planned = "Planned";
    public const string NoticePrepared = "Notice Prepared";
    public const string Held = "Held";
    public const string MinutesPrepared = "Minutes Prepared";
    public const string Completed = "Completed";
    public const string Cancelled = "Cancelled";

    public static readonly string[] All = [Planned, NoticePrepared, Held, MinutesPrepared, Completed, Cancelled];
}
