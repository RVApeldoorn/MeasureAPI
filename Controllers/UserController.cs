using Microsoft.AspNetCore.Mvc;
using MeasurementApi.DTOs;
using MeasurementApi.Services.Interfaces;

namespace MeasurementApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IMeasurementService _measurementService;

        public UserController(IPatientService patientService, IMeasurementService measurementService)
        {
            _patientService = patientService;
            _measurementService = measurementService;
        }

        [HttpGet("allpatients")]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatients();
            if (patients == null || patients.Count() == 0)
            {
                return NotFound("No patients found.");
            }

            return Ok(patients);
        }

        [HttpPost("createmeasurementsession")]
        public async Task<IActionResult> CreateMeasurementSession([FromBody] CreateMeasurementSessionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var session = await _measurementService.CreateMeasurementSession(dto);
            if (session == -1)
            {
                return BadRequest("Failed to create measurement session. Please check the provided data.");
            }
            
            return Ok(new
            {
                message = "Measurement session created.",
                sessions = await _measurementService.GetSessionsByPatient(dto.PatientId)
            });
        }
    }
}
