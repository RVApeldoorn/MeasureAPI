using Microsoft.EntityFrameworkCore;
using MeasurementApi.Models;

namespace MeasurementApi.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Patient> Patients { get; set; }

    public DbSet<MeasurementType> MeasurementTypes { get; set; }
    public DbSet<MeasurementValue> MeasurementValues { get; set; }
    public DbSet<MeasurementRequest> MeasurementRequests { get; set; }
    public DbSet<MeasurementSession> MeasurementSessions { get; set; }
    public DbSet<SetupCode> SetupCodes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
