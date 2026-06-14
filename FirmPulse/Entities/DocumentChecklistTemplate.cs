using System.ComponentModel.DataAnnotations;

namespace FirmPulse.Entities;

public class DocumentChecklistTemplate
{
    public int Id { get; set; }

    public int? ServiceMasterId { get; set; }

    public int? ComplianceTemplateItemId { get; set; }

    [Required]
    [MaxLength(200)]
    public string DocumentName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    public bool IsMandatory { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; } = true;

    public ServiceMaster? ServiceMaster { get; set; }
    public ComplianceTemplateItem? ComplianceTemplateItem { get; set; }
}
