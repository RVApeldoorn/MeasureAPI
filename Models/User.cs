namespace MeasurementApi.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "Doctor";

        public ICollection<MeasurementSession> CreatedMeasurementSessions { get; set; } = new List<MeasurementSession>();
        public ICollection<MeasurementValue> SubmittedMeasurementValues { get; set; } = new List<MeasurementValue>();
    }
}
