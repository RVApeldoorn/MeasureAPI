namespace MeasurementApi.Models
{
    public class SetupCode
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string PatientId { get; set; } = string.Empty;
        public bool Used { get; set; } = false;
        public DateTime ExpiresAt { get; set; }
    }
}
