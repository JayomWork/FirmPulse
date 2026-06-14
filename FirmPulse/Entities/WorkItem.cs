using System.ComponentModel.DataAnnotations;

namespace FirmPulse.Entities;

public class WorkItem
{
    public int Id { get; set; }

    [Required]
    public int FirmId { get; set; }

    [Required]
    public int CompanyClientId { get; set; }

    public int? CompanyCompliancePlanId { get; set; }

    public int? ServiceMasterId { get; set; }

    public int? McaFormMasterId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [MaxLength(20)]
    public string? FinancialYear { get; set; }

    public DateOnly? DueDate { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = WorkItemStatuses.NotStarted;

    [Required]
    [MaxLength(50)]
    public string Priority { get; set; } = WorkItemPriorities.Medium;

    [MaxLength(150)]
    public string? AssignedTo { get; set; }

    public bool RequiresClientDocuments { get; set; }

    [Required]
    [MaxLength(50)]
    public string ClientDocumentsStatus { get; set; } = WorkItemClientDocumentStatuses.NotRequired;

    public bool RequiresMcaFiling { get; set; }

    [Required]
    [MaxLength(50)]
    public string FilingStatus { get; set; } = WorkItemFilingStatuses.NotRequired;

    public DateOnly? CompletedDate { get; set; }

    [MaxLength(1000)]
    public string? Remarks { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Firm? Firm { get; set; }
    public CompanyClient? CompanyClient { get; set; }
    public CompanyCompliancePlan? CompanyCompliancePlan { get; set; }
    public ServiceMaster? ServiceMaster { get; set; }
    public McaFormMaster? McaFormMaster { get; set; }
    public ICollection<WorkItemStatusHistory> StatusHistory { get; set; } = new List<WorkItemStatusHistory>();
    public ICollection<FilingRecord> FilingRecords { get; set; } = new List<FilingRecord>();
    public ICollection<WorkItemDocument> Documents { get; set; } = new List<WorkItemDocument>();
    public ICollection<ClientFollowUp> FollowUps { get; set; } = new List<ClientFollowUp>();
    public ICollection<DocumentRecord> DocumentRecords { get; set; } = new List<DocumentRecord>();
}
