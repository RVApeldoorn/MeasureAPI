namespace MeasurementApi.DTOs;

// Represents a single request in a session
public class MeasurementRequestDto
{
    public int RequestId { get; set; }
    public MeasurementTypeDto MeasurementType { get; set; } = new();
    public List<MeasurementValueDto> MeasurementValue { get; set; } = new();
}