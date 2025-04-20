namespace MeasurementApi.DTOs;

// Represents a patient with their measurement sessions
public class PatientDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<MeasurementSessionDto> MeasurementSessions { get; set; } = new();
}
