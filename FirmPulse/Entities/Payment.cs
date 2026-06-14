using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirmPulse.Entities;

public class Payment
{
    public int Id { get; set; }

    [Required]
    public int InvoiceId { get; set; }

    [DataType(DataType.Date)]
    public DateOnly PaymentDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    [Column(TypeName = "numeric(18,2)")]
    public decimal Amount { get; set; }

    [MaxLength(50)]
    public string? PaymentMode { get; set; }

    [MaxLength(100)]
    public string? ReferenceNumber { get; set; }

    [MaxLength(1000)]
    public string? Remarks { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Invoice? Invoice { get; set; }
}
