
using Clinic.API.Data;
using Clinic.API.Models.DTOs;
using Clinic.API.Models.Entities;
using Clinic.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Clinic.API.Services.Implementations;

public class PatientService : IPatientService
{
    private readonly ApplicationDbContext _context;

    public PatientService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PatientResponse>> GetAllAsync(
        string ownerId)
    {
        return await _context.Patients
            .AsNoTracking()
            .Where(p => p.OwnerId == ownerId)
            .Select(p => new PatientResponse
            {
                Id = p.Id,
                Name = p.Name,
                Egn = p.Egn,
                PhoneNumber = p.PhoneNumber,
                Address = p.Address
            })
            .ToListAsync();
    }

    public async Task<PatientResponse?> GetByIdAsync(
        int id,
        string ownerId)
    {
        var patient = await _context.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(p =>
                p.Id == id && p.OwnerId == ownerId);

        return patient == null ? null : MapToResponse(patient);
    }

    public async Task<PatientResponse> CreateAsync(
        CreatePatientRequest request,
        string ownerId)
    {
        var patient = new Patient
        {
            Name = request.Name,
            Egn = request.Egn,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            OwnerId = ownerId
        };

        _context.Patients.Add(patient);

        await _context.SaveChangesAsync();

        return MapToResponse(patient);
    }

    public async Task<PatientResponse?> UpdateAsync(
        int id,
        UpdatePatientRequest request,
        string ownerId)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p =>
                p.Id == id && p.OwnerId == ownerId);

        if (patient == null)
            return null;

        patient.Name = request.Name;
        patient.Egn = request.Egn;
        patient.PhoneNumber = request.PhoneNumber;
        patient.Address = request.Address;

        await _context.SaveChangesAsync();

        return MapToResponse(patient);
    }

    public async Task<bool> DeleteAsync(int id, string ownerId)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p =>
                p.Id == id && p.OwnerId == ownerId);

        if (patient == null)
            return false;

        _context.Patients.Remove(patient);

        await _context.SaveChangesAsync();

        return true;
    }

    private static PatientResponse MapToResponse(Patient patient)
    {
        return new PatientResponse
        {
            Id = patient.Id,
            Name = patient.Name,
            Egn = patient.Egn,
            PhoneNumber = patient.PhoneNumber,
            Address = patient.Address
        };
    }
}

