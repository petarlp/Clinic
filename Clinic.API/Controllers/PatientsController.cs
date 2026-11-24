
using System.Security.Claims;
using Clinic.API.Models.DTOs;
using Clinic.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    private string? GetOwnerId()
    {
        return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    // GET: api/Patients
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var ownerId = GetOwnerId();

        if (ownerId == null)
            return Unauthorized();

        var patients = await _patientService.GetAllAsync(ownerId);

        return Ok(patients);
    }

    // GET: api/Patients/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ownerId = GetOwnerId();

        if (ownerId == null)
            return Unauthorized();

        var patient = await _patientService.GetByIdAsync(id, ownerId);

        if (patient == null)
            return NotFound(new { message = "Patient not found." });

        return Ok(patient);
    }

    // POST: api/Patients
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreatePatientRequest request)
    {
        var ownerId = GetOwnerId();

        if (ownerId == null)
            return Unauthorized();

        var patient = await _patientService.CreateAsync(request, ownerId);

        return CreatedAtAction(
            nameof(GetById),
            new { id = patient.Id },
            patient);
    }

    // PUT: api/Patients/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdatePatientRequest request)
    {
        var ownerId = GetOwnerId();

        if (ownerId == null)
            return Unauthorized();

        var patient = await _patientService.UpdateAsync(
            id, request, ownerId);

        if (patient == null)
            return NotFound(new { message = "Patient not found." });

        return Ok(patient);
    }

    // DELETE: api/Patients/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ownerId = GetOwnerId();

        if (ownerId == null)
            return Unauthorized();

        var deleted = await _patientService.DeleteAsync(id, ownerId);

        if (!deleted)
            return NotFound(new { message = "Patient not found." });

        return NoContent();
    }
}

