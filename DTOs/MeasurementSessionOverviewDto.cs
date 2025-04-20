namespace MeasurementApi.DTOs;

// Used to represent a session summary
public class MeasurementSessionOverviewDto
{
    public int SessionId { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public List<MeasurementRequestDto> Requests { get; set; } = new();
}