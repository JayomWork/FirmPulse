using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirmPulse.Entities;

public class FilingRecord
{
    public int Id { get; set; }

    [Required]
    public int FirmId { get; set; }

    [Required]
    public int CompanyClientId { get; set; }

    public int? WorkItemId { get; set; }

    [Required]
    public int McaFormMasterId { get; set; }

    [MaxLength(20)]
    public string? FinancialYear { get; set; }

    public DateOnly? DueDate { get; set; }

    public DateOnly? FilingDate { get; set; }

    [MaxLength(50)]
    public string? SRN { get; set; }

    [MaxLength(50)]
    public string? ChallanNumber { get; set; }

    [Column(TypeName = "numeric(18,2)")]
    public decimal? ChallanAmount { get; set; }

    [Required]
    [MaxLength(50)]
    public string McaStatus { get; set; } = FilingRecordStatuses.NotStarted;

    [MaxLength(300)]
    public string? AcknowledgementFilePath { get; set; }

    [MaxLength(1000)]
    public string? Remarks { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Firm? Firm { get; set; }
    public CompanyClient? CompanyClient { get; set; }
    public WorkItem? WorkItem { get; set; }
    public McaFormMaster? McaFormMaster { get; set; }
    public ICollection<DocumentRecord> DocumentRecords { get; set; } = new List<DocumentRecord>();
}
