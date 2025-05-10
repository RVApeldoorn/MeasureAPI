using Microsoft.AspNetCore.Mvc;
using MeasurementApi.DTOs;
using MeasurementApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace MeasurementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly IMeasurementService _measurementService;

    public PatientController(IMeasurementService measurementService)
    {
        _measurementService = measurementService;
    }

    [Authorize]
    [HttpGet("sessions")]
    public async Task<IActionResult> GetSessions()
    {
        var patientId = User.FindFirst("patient_id")?.Value;
        if (string.IsNullOrEmpty(patientId))
            return Unauthorized(new { message = "Invalid or missing patient ID" });

        var sessions = await _measurementService.GetSessionsByPatient(patientId);
        if (sessions == null || !sessions.Any())
            return NotFound($"No sessions found for patient.");

        return Ok(sessions);
    }

    [Authorize]
    [HttpPost("submit")]
    public async Task<IActionResult> SubmitMeasurements([FromBody] MeasurementSubmissionDto dto)
    {
        if (dto == null || dto.Values == null || !dto.Values.Any())
            return BadRequest(new { message = "No measurement values provided." });

        var patientId = User.FindFirst("patient_id")?.Value;
        if (string.IsNullOrEmpty(patientId))
            return Unauthorized(new { message = "Invalid or missing patient ID" });

        try
        {
            await _measurementService.SubmitMeasurement(patientId,dto);
            return Ok(new { message = "Measurements submitted successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Failed to submit measurements.", error = ex.Message });
        }
    }
}
