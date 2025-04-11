using Microsoft.EntityFrameworkCore;
using MeasurementApi.Data;
using MeasurementApi.Models;

namespace MeasurementApi.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly AppDbContext _context;

        public MeasurementService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Measurement> CreateAsync(Measurement measurement)
        {
            _context.Measurements.Add(measurement);
            await _context.SaveChangesAsync();
            return measurement;
        }

        public async Task<IEnumerable<Measurement>> GetByChildAsync(string PatientId)
        {
            return await _context.Measurements.Where(m => m.PatientId == PatientId).ToListAsync();
        }

        public async Task<Measurement?> GetByIdAsync(int id)
        {
            return await _context.Measurements.FindAsync(id);
        }

        public async Task UpdateAsync(int id, Measurement updatedMeasurement)
        {
            var measurement = await _context.Measurements.FindAsync(id);
            if (measurement != null)
            {
                measurement.PatientId = updatedMeasurement.PatientId;
                measurement.Height = updatedMeasurement.Height;
                measurement.Weight = updatedMeasurement.Weight;
                measurement.CreatedAt = updatedMeasurement.CreatedAt;

                _context.Measurements.Update(measurement);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var measurement = await _context.Measurements.FindAsync(id);
            if (measurement != null)
            {
                _context.Measurements.Remove(measurement);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Measurement>> GetAllAsync()
        {
            return await _context.Measurements.ToListAsync();
        }
    }
}
