
using System.ComponentModel.DataAnnotations;

namespace Clinic.API.Models.Entities;

public class Doctor
{
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(30)]
    public string Uin { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string CodSpec { get; set; } = string.Empty;

    [Required]
    [MaxLength(250)]
    public string Address { get; set; } = string.Empty;

    [Required]
    public string OwnerId { get; set; } = string.Empty;

    public ApplicationUser Owner { get; set; } = null!;
}
