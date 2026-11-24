
using Microsoft.AspNetCore.Identity;

namespace Clinic.API.Models.Entities;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
