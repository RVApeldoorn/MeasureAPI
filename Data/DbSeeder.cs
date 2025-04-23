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
                    new Patient { Id = 1, Name = "Klaas Jan" },
                    new Patient { Id = 2, Name = "Corrie de Boer" }
                );
            }

            if (!context.MeasurementTypes.Any())
            {
                context.MeasurementTypes.AddRange(
                    new MeasurementType { Id = 1, Name = "Height", Unit = "cm" },
                    new MeasurementType { Id = 2, Name = "Weight", Unit = "kg" },
                    new MeasurementType { Id = 3, Name = "Temperature", Unit = "°C" }
                );
            }

            if(!context.Auths.Any())
            {
                context.Auths.AddRange(
                    new Auth {Id = 1, PatientId = 1, AuthKey = "123456" },
                    new Auth {Id = 2, PatientId = 2, AuthKey = "abcdef" }
                );
            }

            await context.SaveChangesAsync();
        }
    }
}
