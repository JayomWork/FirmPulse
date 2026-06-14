using System.ComponentModel.DataAnnotations;

namespace FirmPulse.Entities;

public class McaFormMaster
{
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string FormCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string FormName { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [MaxLength(100)]
    public string? ApplicableTo { get; set; }

    [MaxLength(250)]
    public string? DefaultDueRule { get; set; }

    public bool IsAnnualForm { get; set; }

    public bool IsEventBasedForm { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ComplianceTemplateItem> ComplianceTemplateItems { get; set; } = new List<ComplianceTemplateItem>();
    public ICollection<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
    public ICollection<FilingRecord> FilingRecords { get; set; } = new List<FilingRecord>();
}
