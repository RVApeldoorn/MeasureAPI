using MeasurementApi.Models;

namespace MeasurementApi.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Id = 1, Name = "Henk de Vries", Email = "henk@mail.nl" },
                    new User { Id = 2, Name = "Piet Smit", Email = "piet@mail.nl" }
                );
            }

            if (!context.Patients.Any())
            {
                context.Patients.AddRange(
                    new Patient { Id = "patient_1", Name = "Klaas Jan" },
                    new Patient { Id = "patient_2", Name = "Corrie de Boer" }
                );
            }

            if (!context.MeasurementTypes.Any())
            {
                context.MeasurementTypes.AddRange(
                    new MeasurementType { Id = 1, Name = "Height", Unit = "mm" },
                    new MeasurementType { Id = 2, Name = "Weight", Unit = "kg" },
                    new MeasurementType { Id = 3, Name = "Temperature", Unit = "°C" }
                );
            }

            if (!context.SetupCodes.Any())
            {
                context.SetupCodes.AddRange(
                    new SetupCode { PatientId = "patient_1", Code = "ABC123", ExpiresAt = DateTime.MaxValue, Used = false },
                    new SetupCode { PatientId = "patient_2", Code = "XYZ789", ExpiresAt = DateTime.MaxValue, Used = false }
                );
            }

            await context.SaveChangesAsync();
        }
    }
}