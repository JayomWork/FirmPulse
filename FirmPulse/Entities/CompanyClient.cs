using System.ComponentModel.DataAnnotations;

namespace FirmPulse.Entities;

public class CompanyClient
{
    public int Id { get; set; }

    [Required]
    public int FirmId { get; set; }

    [Required]
    [MaxLength(200)]
    public string CompanyName { get; set; } = string.Empty;

    [Required]
    [MaxLength(21)]
    public string CIN { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    public string PAN { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    public DateOnly? IncorporationDate { get; set; }

    [Required]
    [MaxLength(500)]
    public string RegisteredOfficeAddress { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string CompanyType { get; set; } = CompanyClientTypes.PrivateLimited;

    [MaxLength(100)]
    public string? RegistrationNumber { get; set; }

    [MaxLength(100)]
    public string? City { get; set; }

    [MaxLength(100)]
    public string? State { get; set; }

    [MaxLength(20)]
    public string? PinCode { get; set; }

    [Required]
    [MaxLength(100)]
    public string Status { get; set; } = CompanyClientStatuses.Active;

    [Required]
    [MaxLength(150)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    [MaxLength(20)]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string ContactPersonName { get; set; } = string.Empty;

    public int? FinancialYearEndMonth { get; set; }

    public int? FinancialYearEndDay { get; set; }

    public DateOnly? LastAGMDate { get; set; }

    public decimal? AuthorizedCapital { get; set; }

    public decimal? PaidUpCapital { get; set; }

    public DateOnly? FinancialYearStart { get; set; }

    public DateOnly? FinancialYearEnd { get; set; }

    [MaxLength(100)]
    public string? CompanyClass { get; set; }

    public bool IsListed { get; set; }

    public bool HasShareCapital { get; set; } = true;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Firm? Firm { get; set; }
    public ICollection<Director> Directors { get; set; } = new List<Director>();
    public ICollection<ComplianceTask> ComplianceTasks { get; set; } = new List<ComplianceTask>();
    public ICollection<DocumentRecord> DocumentRecords { get; set; } = new List<DocumentRecord>();
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    public ICollection<CompanyCompliancePlan> CompanyCompliancePlans { get; set; } = new List<CompanyCompliancePlan>();
    public ICollection<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
    public ICollection<FilingRecord> FilingRecords { get; set; } = new List<FilingRecord>();
    public ICollection<WorkItemDocument> WorkItemDocuments { get; set; } = new List<WorkItemDocument>();
    public ICollection<ClientFollowUp> ClientFollowUps { get; set; } = new List<ClientFollowUp>();
    public ICollection<CompanyMeeting> CompanyMeetings { get; set; } = new List<CompanyMeeting>();
    public ICollection<GeneratedDocument> GeneratedDocuments { get; set; } = new List<GeneratedDocument>();
}
