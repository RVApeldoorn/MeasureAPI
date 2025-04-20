namespace MeasurementApi.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public ICollection<MeasurementSession> MeasurementSessions { get; set; } = new List<MeasurementSession>();
    }
}
