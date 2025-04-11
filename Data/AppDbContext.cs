using Microsoft.EntityFrameworkCore;
using MeasurementApi.Models;

namespace MeasurementApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Measurement> Measurements => Set<Measurement>();
}
