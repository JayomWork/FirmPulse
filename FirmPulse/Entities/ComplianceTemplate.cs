using System.ComponentModel.DataAnnotations;

namespace FirmPulse.Entities;

public class ComplianceTemplate
{
    public int Id { get; set; }

    public int? FirmId { get; set; }

    [Required]
    [MaxLength(200)]
    public string TemplateName { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(100)]
    public string CompanyType { get; set; } = string.Empty;

    public bool FinancialYearBased { get; set; }

    public bool IsSystemTemplate { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Firm? Firm { get; set; }
    public ICollection<ComplianceTemplateItem> Items { get; set; } = new List<ComplianceTemplateItem>();
    public ICollection<CompanyCompliancePlan> CompanyCompliancePlans { get; set; } = new List<CompanyCompliancePlan>();
}
