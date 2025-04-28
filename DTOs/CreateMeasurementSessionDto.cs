namespace MeasurementApi.DTOs;

// Used to create a new measurement session, containing multiple requests
public class CreateMeasurementSessionDto
{
    public int CreatedByUserId { get; set; }
    public string PatientId { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public List<CreateMeasurementRequestDto> Requests { get; set; } = new();
}