namespace MeasurementApi.DTOs;

// Represents a full session with all request details
public class MeasurementSessionDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime DueDate { get; set; }
    public List<MeasurementRequestDto> Requests { get; set; } = new();
}