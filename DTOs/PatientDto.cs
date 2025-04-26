namespace MeasurementApi.DTOs;

// Represents a patient with their measurement sessions
public class PatientDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<MeasurementSessionDto> MeasurementSessions { get; set; } = new();
}
