namespace Clinic.API.Models.DTOs;

public class PatientResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Egn { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }
}