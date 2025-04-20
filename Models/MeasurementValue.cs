namespace MeasurementApi.Models
{
    public class MeasurementValue
    {
        public int Id { get; set; }

        public int MeasurementRequestId { get; set; }
        public MeasurementRequest? MeasurementRequest { get; set; }

        public decimal Value { get; set; }
        public DateTime TakenAt { get; set; }

        public string Source { get; set; } = "app";

        public string? Note { get; set; }
    }
}
