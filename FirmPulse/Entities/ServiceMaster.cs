using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirmPulse.Entities;

public class ServiceMaster
{
    public int Id { get; set; }

    [Required]
    public int ServiceCategoryId { get; set; }

    [Required]
    [MaxLength(30)]
    public string ServiceCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string ServiceName { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Column(TypeName = "numeric(18,2)")]
    public decimal? DefaultFee { get; set; }

    public int? DefaultDueDays { get; set; }

    public bool IsRecurring { get; set; }

    [MaxLength(50)]
    public string? RecurringType { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ServiceCategory? ServiceCategory { get; set; }
    public ICollection<ComplianceTemplateItem> ComplianceTemplateItems { get; set; } = new List<ComplianceTemplateItem>();
    public ICollection<DocumentChecklistTemplate> DocumentChecklistTemplates { get; set; } = new List<DocumentChecklistTemplate>();
    public ICollection<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
}
