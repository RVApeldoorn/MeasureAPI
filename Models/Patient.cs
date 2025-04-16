namespace MeasurementApi.Models;

public class Patient
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public required ICollection<MeasurementValue> MeasurementValues { get; set; }
    public required ICollection<MeasurementRequest> MeasurementRequests { get; set; }
}
