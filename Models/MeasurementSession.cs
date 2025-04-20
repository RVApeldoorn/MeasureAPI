namespace MeasurementApi.Models
{
    public class MeasurementSession
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }

        public bool IsCompleted { get; set; } = false;
        
        public DateTime DueDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<MeasurementRequest> MeasurementRequests { get; set; } = new List<MeasurementRequest>();
    }
}
