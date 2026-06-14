using System.ComponentModel.DataAnnotations;

namespace FirmPulse.Entities;

public class ClientFollowUp
{
    public int Id { get; set; }

    [Required]
    public int FirmId { get; set; }

    [Required]
    public int CompanyClientId { get; set; }

    public int? WorkItemId { get; set; }

    public DateOnly FollowUpDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    [Required]
    [MaxLength(50)]
    public string FollowUpType { get; set; } = FollowUpTypes.Call;

    [Required]
    [MaxLength(200)]
    public string Subject { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Notes { get; set; }

    public DateOnly? NextFollowUpDate { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = ClientFollowUpStatuses.Open;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Firm? Firm { get; set; }
    public CompanyClient? CompanyClient { get; set; }
    public WorkItem? WorkItem { get; set; }
}
