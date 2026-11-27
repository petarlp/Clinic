namespace Clinic.API.Models.DTOs.Doctors;


public class DoctorResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Uin { get; set; } = string.Empty;
    public string CodSpec { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
