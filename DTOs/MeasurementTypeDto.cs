namespace MeasurementApi.DTOs;

// Used to represent a measurement type like "length" and "weight"
public class MeasurementTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
}
