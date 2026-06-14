using System.ComponentModel.DataAnnotations;

namespace FirmPulse.Entities;

public class CompanyCompliancePlan
{
    public int Id { get; set; }

    [Required]
    public int FirmId { get; set; }

    [Required]
    public int CompanyClientId { get; set; }

    [Required]
    public int ComplianceTemplateId { get; set; }

    [Required]
    [MaxLength(20)]
    public string FinancialYear { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string PlanName { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public DateOnly? AGMDate { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = CompanyCompliancePlanStatuses.Draft;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Firm? Firm { get; set; }
    public CompanyClient? CompanyClient { get; set; }
    public ComplianceTemplate? ComplianceTemplate { get; set; }
    public ICollection<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
    public ICollection<CompanyMeeting> Meetings { get; set; } = new List<CompanyMeeting>();
}
