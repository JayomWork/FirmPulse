using System.ComponentModel.DataAnnotations;

namespace FirmPulse.Entities;

public class DocumentTemplate
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string TemplateName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string TemplateCategory { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Required]
    public string TemplateContent { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<DocumentTemplateField> Fields { get; set; } = new List<DocumentTemplateField>();
    public ICollection<GeneratedDocument> GeneratedDocuments { get; set; } = new List<GeneratedDocument>();
}
