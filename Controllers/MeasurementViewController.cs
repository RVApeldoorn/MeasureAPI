using Microsoft.AspNetCore.Mvc;
using MeasurementApi.Services;

namespace MeasurementApi.Controllers
{
    [Route("measurements")]
    public class MeasurementViewController : Controller
    {
        private readonly IMeasurementService _measurementService;

        public MeasurementViewController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var measurements = await _measurementService.GetAllAsync();
            return View(measurements);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var measurement = await _measurementService.GetByIdAsync(id);
            if (measurement == null)
            {
                return NotFound();
            }

            return View(measurement);
        }
    }
}