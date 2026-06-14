using System.ComponentModel.DataAnnotations;

namespace FirmPulse.Entities;

public class WorkItemStatusHistory
{
    public int Id { get; set; }

    [Required]
    public int WorkItemId { get; set; }

    [MaxLength(50)]
    public string? OldStatus { get; set; }

    [Required]
    [MaxLength(50)]
    public string NewStatus { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Remarks { get; set; }

    [MaxLength(150)]
    public string? ChangedBy { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public WorkItem? WorkItem { get; set; }
}
