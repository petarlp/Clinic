
using Clinic.API.Models.DTOs.Doctors;

namespace Clinic.API.Services.Interfaces;

public interface IDoctorService
{
    Task<List<DoctorResponse>> GetAllAsync(string ownerId);

    Task<DoctorResponse?> GetByIdAsync(int id, string ownerId);

    Task<DoctorResponse> CreateAsync(CreateDoctorRequest request, string ownerId);

    Task<DoctorResponse?> UpdateAsync(int id, UpdateDoctorRequest request, string ownerId);

    Task<bool> DeleteAsync(int id, string ownerId);
}
