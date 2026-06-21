using System.ComponentModel.DataAnnotations;

namespace FirmPulse.Entities;

public class GeneratedDocument
{
    public int Id { get; set; }

    public int CompanyClientId { get; set; }

    public int DocumentTemplateId { get; set; }

    [Required]
    [MaxLength(250)]
    public string DocumentTitle { get; set; } = string.Empty;

    [Required]
    public string GeneratedContent { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? WordFilePath { get; set; }

    [MaxLength(500)]
    public string? PdfFilePath { get; set; }

    [MaxLength(150)]
    public string? GeneratedBy { get; set; }

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    public CompanyClient? CompanyClient { get; set; }
    public DocumentTemplate? DocumentTemplate { get; set; }
    public ICollection<GeneratedDocumentValue> Values { get; set; } = new List<GeneratedDocumentValue>();
}
