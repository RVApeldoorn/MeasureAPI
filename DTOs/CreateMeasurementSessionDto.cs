namespace MeasurementApi.DTOs;

// Used to create a new measurement session, containing multiple requests
public class CreateMeasurementSessionDto
{
    public int CreatedByUserId { get; set; }
    public int PatientId { get; set; }
    public DateTime DueDate { get; set; }
    public List<CreateMeasurementRequestDto> Requests { get; set; } = new();
}