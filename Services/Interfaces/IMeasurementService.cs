using MeasurementApi.DTOs;

namespace MeasurementApi.Services.Interfaces;

public interface IMeasurementService
{
    Task<int> CreateMeasurementSession(CreateMeasurementSessionDto dto);
    Task<PatientSessionsOverviewDto> GetSessionsByPatient(string patientId);

    Task SubmitMeasurement(string PatientId, MeasurementSubmissionDto dto);

    Task<IEnumerable<MeasurementTypeDto>> GetAllMeasurementTypes();
}
