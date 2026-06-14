using System.ComponentModel.DataAnnotations;

namespace FirmPulse.Entities;

public class Firm
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Phone { get; set; }

    [MaxLength(500)]
    public string? Address { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<CompanyClient> CompanyClients { get; set; } = new List<CompanyClient>();
    public ICollection<ComplianceTask> ComplianceTasks { get; set; } = new List<ComplianceTask>();
    public ICollection<DocumentRecord> DocumentRecords { get; set; } = new List<DocumentRecord>();
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
