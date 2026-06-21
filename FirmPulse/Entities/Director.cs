using System.ComponentModel.DataAnnotations;

namespace FirmPulse.Entities;

public class Director
{
    public int Id { get; set; }

    [Required]
    public int CompanyClientId { get; set; }

    [Required]
    [MaxLength(150)]
    public string FullName { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? DIN { get; set; }

    [MaxLength(10)]
    public string? PAN { get; set; }

    [MaxLength(150)]
    [EmailAddress]
    public string? Email { get; set; }

    [MaxLength(20)]
    public string? Phone { get; set; }

    [MaxLength(500)]
    public string? Address { get; set; }

    [MaxLength(100)]
    public string? Designation { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? AppointmentDate { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? ResignationDate { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public CompanyClient? CompanyClient { get; set; }
}
