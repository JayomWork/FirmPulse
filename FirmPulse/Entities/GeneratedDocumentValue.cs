using System.ComponentModel.DataAnnotations;

namespace FirmPulse.Entities;

public class GeneratedDocumentValue
{
    public int Id { get; set; }

    public int GeneratedDocumentId { get; set; }

    [Required]
    [MaxLength(100)]
    public string FieldKey { get; set; } = string.Empty;

    public string? FieldValue { get; set; }

    public GeneratedDocument? GeneratedDocument { get; set; }
}
