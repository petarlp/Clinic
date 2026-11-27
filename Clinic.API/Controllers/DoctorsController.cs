
using System.Security.Claims;
using Clinic.API.Models.DTOs.Doctors;
using Clinic.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    private string OwnerId =>
        User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var doctors = await _doctorService.GetAllAsync(OwnerId);
        return Ok(doctors);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var doctor = await _doctorService.GetByIdAsync(id, OwnerId);

        if (doctor == null)
            return NotFound();

        return Ok(doctor);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDoctorRequest request)
    {
        var doctor = await _doctorService.CreateAsync(request, OwnerId);

        return CreatedAtAction(
            nameof(GetById),
            new { id = doctor.Id },
            doctor
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateDoctorRequest request)
    {
        var doctor = await _doctorService.UpdateAsync(id, request, OwnerId);

        if (doctor == null)
            return NotFound();

        return Ok(doctor);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _doctorService.DeleteAsync(id, OwnerId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
