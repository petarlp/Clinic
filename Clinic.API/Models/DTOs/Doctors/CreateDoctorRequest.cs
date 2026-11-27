
using System.ComponentModel.DataAnnotations;

namespace Clinic.API.Models.DTOs.Doctors;

public class CreateDoctorRequest
{
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
}
