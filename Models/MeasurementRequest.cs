namespace MeasurementApi.Models
{
    public class MeasurementRequest
    {
        public int Id { get; set; }

        public int MeasurementSessionId { get; set; }
        public MeasurementSession? MeasurementSession { get; set; }

        public int MeasurementTypeId { get; set; }
        public MeasurementType MeasurementType { get; set; } = null!;

        public ICollection<MeasurementValue> MeasurementValues { get; set; } = new List<MeasurementValue>();
    }
}