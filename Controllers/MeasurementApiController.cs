using Microsoft.AspNetCore.Mvc;
using MeasurementApi.Models;
using MeasurementApi.Services;

namespace MeasurementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementService _measurementService;

        public MeasurementController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Measurement measurement)
        {
            var createdMeasurement = await _measurementService.CreateAsync(measurement);
            return Ok(createdMeasurement);
        }

        [HttpGet("child/{childId}")]
        public async Task<ActionResult> GetByChild(string childId)
        {
            var data = await _measurementService.GetByChildAsync(childId);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Measurement>> GetById(int id)
        {
            var measurement = await _measurementService.GetByIdAsync(id);
            if (measurement == null)
            {
                return NotFound();
            }
            return Ok(measurement);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Measurement updatedMeasurement)
        {
            await _measurementService.UpdateAsync(id, updatedMeasurement);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _measurementService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Measurement>>> GetAll()
        {
            var measurements = await _measurementService.GetAllAsync();
            return Ok(measurements);
        }
    }
}
