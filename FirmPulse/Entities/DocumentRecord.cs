using System.ComponentModel.DataAnnotations;

namespace FirmPulse.Entities;

public class DocumentRecord
{
    public int Id { get; set; }

    [Required]
    public int FirmId { get; set; }

    [Required]
    public int CompanyClientId { get; set; }

    public int? ComplianceTaskId { get; set; }

    [Required]
    [MaxLength(200)]
    public string DocumentName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? DocumentType { get; set; }

    [MaxLength(300)]
    public string? FilePath { get; set; }

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(1000)]
    public string? Remarks { get; set; }

    public Firm? Firm { get; set; }
    public CompanyClient? CompanyClient { get; set; }
    public ComplianceTask? ComplianceTask { get; set; }
}
