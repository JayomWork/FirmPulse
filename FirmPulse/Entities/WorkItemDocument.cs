using System.ComponentModel.DataAnnotations;

namespace FirmPulse.Entities;

public class WorkItemDocument
{
    public int Id { get; set; }

    [Required]
    public int FirmId { get; set; }

    [Required]
    public int CompanyClientId { get; set; }

    [Required]
    public int WorkItemId { get; set; }

    [Required]
    [MaxLength(200)]
    public string DocumentName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? DocumentType { get; set; }

    public bool IsMandatory { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = WorkItemDocumentStatuses.PendingFromClient;

    [MaxLength(300)]
    public string? FilePath { get; set; }

    public DateOnly? ReceivedDate { get; set; }

    public DateOnly? ReviewedDate { get; set; }

    [MaxLength(1000)]
    public string? Remarks { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Firm? Firm { get; set; }
    public CompanyClient? CompanyClient { get; set; }
    public WorkItem? WorkItem { get; set; }
}
