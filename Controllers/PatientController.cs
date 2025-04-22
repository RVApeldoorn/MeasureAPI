using Microsoft.AspNetCore.Mvc;
using MeasurementApi.DTOs;
using MeasurementApi.Services.Interfaces;

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

    [HttpGet("{patientId}/sessions")]
    public async Task<IActionResult> GetSessions(int patientId)
    {
        var sessions = await _measurementService.GetSessionsByPatient(patientId);
        if (sessions == null || sessions.Count() == 0)
        {
            return NotFound($"No sessions found for patient with ID {patientId}.");
        }
        
        return Ok(sessions);
    }

    [BearerTokenFilter]
    [HttpPost("{patientId}/submit")]
    public async Task<IActionResult> SubmitMeasurements([FromRoute] int patientId, [FromBody] MeasurementSubmissionDto dto)
    {
        if (dto == null || dto.Values == null || !dto.Values.Any())
        {
            return BadRequest(new { message = "No measurement values provided." });
        }

        try
        {
            await _measurementService.SubmitMeasurement(patientId, dto);
            return Ok(new { message = "Measurements submitted successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Failed to submit measurements.", error = ex.Message });
        }
    }
}
