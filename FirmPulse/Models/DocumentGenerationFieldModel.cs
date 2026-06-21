namespace FirmPulse.Models;

public class DocumentGenerationFieldModel
{
    public string FieldKey { get; set; } = string.Empty;
    public string FieldLabel { get; set; } = string.Empty;
    public string FieldType { get; set; } = "Text";
    public string? DataSource { get; set; }
    public bool IsRequired { get; set; } = true;
    public int DisplayOrder { get; set; }
    public string? DefaultValue { get; set; }
    public string? FieldValue { get; set; }
    public bool IsAutoFilled { get; set; }
}
