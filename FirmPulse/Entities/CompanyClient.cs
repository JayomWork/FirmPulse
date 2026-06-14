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

    [MaxLength(21)]
    public string? CIN { get; set; }

    [MaxLength(10)]
    public string? PAN { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? IncorporationDate { get; set; }

    [MaxLength(500)]
    public string? RegisteredOfficeAddress { get; set; }

    [MaxLength(100)]
    public string? CompanyType { get; set; }

    [MaxLength(100)]
    public string? Status { get; set; }

    [MaxLength(150)]
    [EmailAddress]
    public string? Email { get; set; }

    [MaxLength(20)]
    public string? Phone { get; set; }

    [MaxLength(150)]
    public string? ContactPersonName { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Firm? Firm { get; set; }
    public ICollection<Director> Directors { get; set; } = new List<Director>();
    public ICollection<ComplianceTask> ComplianceTasks { get; set; } = new List<ComplianceTask>();
    public ICollection<DocumentRecord> DocumentRecords { get; set; } = new List<DocumentRecord>();
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
