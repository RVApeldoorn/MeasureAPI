using MeasurementApi.DTOs;

namespace MeasurementApi.Services.Interfaces;

public interface IMeasurementService
{
    Task<int> CreateMeasurementSession(CreateMeasurementSessionDto dto);
    Task<IEnumerable<MeasurementSessionOverviewDto>> GetSessionsByPatient(int patientId);

    Task SubmitMeasurement(int PatientId, MeasurementSubmissionDto dto);

    Task<IEnumerable<MeasurementTypeDto>> GetAllMeasurementTypes();
}
