using MeasurementApi.Models;
namespace MeasurementApi.Services
{
    public interface IMeasurementService
    {
        Task<Measurement> CreateAsync(Measurement measurement);
        Task<IEnumerable<Measurement>> GetByChildAsync(string childId);
        Task<Measurement?> GetByIdAsync(int id);
        Task UpdateAsync(int id, Measurement measurement);
        Task DeleteAsync(int id);
        Task<IEnumerable<Measurement>> GetAllAsync();
    }
}
