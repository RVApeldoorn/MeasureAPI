namespace MeasurementApi.DTOs;

public class PatientSessionsOverviewDto
{
    public string PatientName { get; set; } = string.Empty;
    public List<MeasurementSessionOverviewDto> Sessions { get; set; } = new();
}
