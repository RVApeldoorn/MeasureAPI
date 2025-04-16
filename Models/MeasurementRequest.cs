namespace MeasurementApi.Models
{
    public class MeasurementRequest
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public required Patient Patient { get; set; }

        public int RequestedByUserId { get; set; }
        public required User RequestedByUser { get; set; }

        public int MeasurementTypeId { get; set; }
        public required MeasurementType MeasurementType { get; set; }

        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;

        public ICollection<MeasurementValue> MeasurementValues { get; set; } = new List<MeasurementValue>();
    }
}