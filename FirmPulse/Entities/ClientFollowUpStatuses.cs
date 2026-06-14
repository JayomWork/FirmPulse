namespace FirmPulse.Entities;

public static class ClientFollowUpStatuses
{
    public const string Open = "Open";
    public const string Done = "Done";
    public const string NoResponse = "No Response";
    public const string WaitingClient = "Waiting Client";

    public static readonly string[] All = [Open, Done, NoResponse, WaitingClient];
}
