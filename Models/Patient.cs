namespace MeasurementApi.Models
{
    public class Patient
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        
        public ICollection<MeasurementSession> MeasurementSessions { get; set; } = new List<MeasurementSession>();
    }
}
