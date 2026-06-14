using System.ComponentModel.DataAnnotations;

namespace FirmPulse.Entities;

public class CompanyMeeting
{
    public int Id { get; set; }

    [Required]
    public int FirmId { get; set; }

    [Required]
    public int CompanyClientId { get; set; }

    public int? CompanyCompliancePlanId { get; set; }

    [Required]
    [MaxLength(50)]
    public string MeetingType { get; set; } = CompanyMeetingTypes.BoardMeeting;

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public DateOnly? MeetingDate { get; set; }

    public DateOnly? DueDate { get; set; }

    [MaxLength(200)]
    public string? VenueOrMode { get; set; }

    public bool NoticePrepared { get; set; }

    public bool AgendaPrepared { get; set; }

    public bool MinutesPrepared { get; set; }

    public bool SignedCopyReceived { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = CompanyMeetingStatuses.Planned;

    [MaxLength(1000)]
    public string? Remarks { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Firm? Firm { get; set; }
    public CompanyClient? CompanyClient { get; set; }
    public CompanyCompliancePlan? CompanyCompliancePlan { get; set; }
}
