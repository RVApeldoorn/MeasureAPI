using MeasurementApi.DTOs;

namespace MeasurementApi.Services.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDto>> GetAllPatients();
    }
}
