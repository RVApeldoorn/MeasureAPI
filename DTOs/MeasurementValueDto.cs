namespace MeasurementApi.DTOs;

// Individual value input from a patient (one per measurement request)
public class MeasurementValueDto
{
    public int Id { get; set; }
    public int MeasurementRequestId { get; set; }
    public decimal Value { get; set; }
    public string? Note { get; set; }
    public DateTime TakenAt { get; set; } = DateTime.UtcNow;
}