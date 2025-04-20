namespace MeasurementApi.DTOs;

// Represents a single measurement value submission
public class MeasurementSubmissionValueDto
{
    public int MeasurementRequestId { get; set; }
    public int MeasurementTypeId { get; set; }
    public double Value { get; set; }
    public DateTime TakenAt { get; set; }
}