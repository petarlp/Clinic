
using Clinic.API.Data;
using Clinic.API.Models.DTOs.Doctors;
using Clinic.API.Models.Entities;
using Clinic.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Clinic.API.Services;

public class DoctorService : IDoctorService
{
    private readonly ApplicationDbContext _context;

    public DoctorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<DoctorResponse>> GetAllAsync(string ownerId)
    {
        return await _context.Doctors
            .Where(d => d.OwnerId == ownerId)
            .Select(d => new DoctorResponse
            {
                Id = d.Id,
                Name = d.Name,
                Uin = d.Uin,
                CodSpec = d.CodSpec,
                Address = d.Address
            })
            .ToListAsync();
    }

    public async Task<DoctorResponse?> GetByIdAsync(int id, string ownerId)
    {
        return await _context.Doctors
            .Where(d => d.Id == id && d.OwnerId == ownerId)
            .Select(d => new DoctorResponse
            {
                Id = d.Id,
                Name = d.Name,
                Uin = d.Uin,
                CodSpec = d.CodSpec,
                Address = d.Address
            })
            .FirstOrDefaultAsync();
    }

    public async Task<DoctorResponse> CreateAsync(CreateDoctorRequest request, string ownerId)
    {
        var doctor = new Doctor
        {
            Name = request.Name,
            Uin = request.Uin,
            CodSpec = request.CodSpec,
            Address = request.Address,
            OwnerId = ownerId
        };

        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();

        return new DoctorResponse
        {
            Id = doctor.Id,
            Name = doctor.Name,
            Uin = doctor.Uin,
            CodSpec = doctor.CodSpec,
            Address = doctor.Address
        };
    }

    public async Task<DoctorResponse?> UpdateAsync(int id, UpdateDoctorRequest request, string ownerId)
    {
        var doctor = await _context.Doctors
            .FirstOrDefaultAsync(d => d.Id == id && d.OwnerId == ownerId);

        if (doctor == null)
            return null;

        doctor.Name = request.Name;
        doctor.Uin = request.Uin;
        doctor.CodSpec = request.CodSpec;
        doctor.Address = request.Address;

        await _context.SaveChangesAsync();

        return new DoctorResponse
        {
            Id = doctor.Id,
            Name = doctor.Name,
            Uin = doctor.Uin,
            CodSpec = doctor.CodSpec,
            Address = doctor.Address
        };
    }

    public async Task<bool> DeleteAsync(int id, string ownerId)
    {
        var doctor = await _context.Doctors
            .FirstOrDefaultAsync(d => d.Id == id && d.OwnerId == ownerId);

        if (doctor == null)
            return false;

        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();

        return true;
    }
}
