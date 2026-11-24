
using System.ComponentModel.DataAnnotations;

namespace Clinic.API.Models.Entities;

public class Patient
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "EGN is required.")]
    [StringLength(10, MinimumLength = 10)]
    [RegularExpression(@"^\d{10}$",
        ErrorMessage = "EGN must contain exactly 10 digits.")]
    public string Egn { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [MaxLength(250)]
    public string? Address { get; set; }

    [Required]
    public string OwnerId { get; set; } = string.Empty;

    public ApplicationUser Owner { get; set; } = null!;
}
