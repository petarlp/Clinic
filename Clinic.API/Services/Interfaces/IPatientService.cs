using Clinic.API.Models.DTOs;

namespace Clinic.API.Services.Interfaces;

public interface IPatientService
{
    Task<IEnumerable<PatientResponse>> GetAllAsync(string ownerId);

    Task<PatientResponse?> GetByIdAsync(int id, string ownerId);

    Task<PatientResponse> CreateAsync(
        CreatePatientRequest request,
        string ownerId);

    Task<PatientResponse?> UpdateAsync(
        int id,
        UpdatePatientRequest request,
        string ownerId);

    Task<bool> DeleteAsync(int id, string ownerId);
}

