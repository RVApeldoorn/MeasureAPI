namespace MeasurementApi.Models
{
    public class MeasurementValue
    {
        public int Id { get; set; }

        public int MeasurementRequestId { get; set; }
        public required MeasurementRequest MeasurementRequest { get; set; }

        public int PatientId { get; set; }
        public required Patient Patient { get; set; }

        public int MeasurementTypeId { get; set; }
        public required MeasurementType MeasurementType { get; set; }

        public decimal Value { get; set; }
        public DateTime TakenAt { get; set; }
        public string Source { get; set; } = "app";
    }
}