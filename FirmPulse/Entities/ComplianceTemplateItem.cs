using System.ComponentModel.DataAnnotations;

namespace FirmPulse.Entities;

public class ComplianceTemplateItem
{
    public int Id { get; set; }

    [Required]
    public int ComplianceTemplateId { get; set; }

    public int? ServiceMasterId { get; set; }

    public int? McaFormMasterId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public int SequenceNo { get; set; }

    public int? DefaultDueOffsetDays { get; set; }

    [Required]
    [MaxLength(50)]
    public string DueDateBasedOn { get; set; } = DueDateBasedOnTypes.Manual;

    public bool RequiresFiling { get; set; }

    public bool RequiresDocuments { get; set; }

    public bool RequiresMeeting { get; set; }

    [Required]
    [MaxLength(50)]
    public string Priority { get; set; } = WorkItemPriorities.Medium;

    public bool IsActive { get; set; } = true;

    public ComplianceTemplate? ComplianceTemplate { get; set; }
    public ServiceMaster? ServiceMaster { get; set; }
    public McaFormMaster? McaFormMaster { get; set; }
    public ICollection<DocumentChecklistTemplate> DocumentChecklistTemplates { get; set; } = new List<DocumentChecklistTemplate>();
}
