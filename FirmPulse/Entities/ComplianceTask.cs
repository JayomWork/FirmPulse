using System.ComponentModel.DataAnnotations;

namespace FirmPulse.Entities;

public class ComplianceTask
{
    public int Id { get; set; }

    [Required]
    public int FirmId { get; set; }

    [Required]
    public int CompanyClientId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(100)]
    public string ComplianceType { get; set; } = ComplianceTaskTypes.AnnualFiling;

    [DataType(DataType.Date)]
    public DateOnly DueDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = ComplianceTaskStatuses.Pending;

    [Required]
    [MaxLength(50)]
    public string Priority { get; set; } = ComplianceTaskPriorities.Medium;

    [Required]
    [MaxLength(150)]
    public string AssignedTo { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateOnly? CompletedDate { get; set; }

    [MaxLength(1000)]
    public string? Remarks { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Firm? Firm { get; set; }
    public CompanyClient? CompanyClient { get; set; }
    public ICollection<DocumentRecord> DocumentRecords { get; set; } = new List<DocumentRecord>();
}
