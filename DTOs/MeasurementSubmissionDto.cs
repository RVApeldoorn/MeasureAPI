namespace MeasurementApi.DTOs;

// Sent by the patient when submitting one or more values for existing requests
public class MeasurementSubmissionDto
{
    public int sessionId { get; set; }
    public List<MeasurementValueDto> Values { get; set; } = new();
}