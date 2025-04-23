namespace MeasurementApi.Models
{
    public class Auth
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public required string AuthKey { get; set; }
    }
}