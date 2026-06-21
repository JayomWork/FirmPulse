using System.ComponentModel.DataAnnotations;

namespace FirmPulse.Entities;

public class DocumentTemplateField
{
    public int Id { get; set; }

    public int DocumentTemplateId { get; set; }

    [Required]
    [MaxLength(100)]
    public string FieldKey { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string FieldLabel { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string FieldType { get; set; } = "Text";

    [MaxLength(100)]
    public string? DataSource { get; set; }

    public bool IsRequired { get; set; } = true;

    public int DisplayOrder { get; set; }

    [MaxLength(500)]
    public string? DefaultValue { get; set; }

    public DocumentTemplate? DocumentTemplate { get; set; }
}
