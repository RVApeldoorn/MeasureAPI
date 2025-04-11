namespace MeasurementApi.Models;

public class Measurement
{
    public int Id { get; set; }
    public string PatientId { get; set; } = string.Empty;
    public double Height { get; set; }
    public string HeightUnit { get; set; } = "cm";
    public double Weight { get; set; }
    public string WeightUnit { get; set; } = "kg"; 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}
