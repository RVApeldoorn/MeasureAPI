using MeasurementApi.Data;
using MeasurementApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MeasurementApi.DTOs;

namespace MeasurementApi.Services
{
    public class PatientService : IPatientService
    {
        private readonly AppDbContext _db;

        public PatientService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatients()
        {
            return await _db.Patients
                .Include(p => p.MeasurementSessions)
                    .ThenInclude(ms => ms.MeasurementRequests)
                        .ThenInclude(r => r.MeasurementValues)
                .Include(p => p.MeasurementSessions)
                    .ThenInclude(ms => ms.MeasurementRequests)
                        .ThenInclude(r => r.MeasurementType)
                .Select(p => new PatientDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    MeasurementSessions = p.MeasurementSessions.Select(ms => new MeasurementSessionDto
                    {
                        Id = ms.Id,
                        PatientId = ms.PatientId,
                        DueDate = ms.DueDate,
                        IsCompleted = ms.IsCompleted,
                        Requests = ms.MeasurementRequests.Select(r => new MeasurementRequestDto
                        {
                            RequestId = r.Id,
                            MeasurementType = new MeasurementTypeDto
                            {
                                Id = r.MeasurementType.Id,
                                Name = r.MeasurementType.Name,
                                Unit = r.MeasurementType.Unit
                            },
                            MeasurementValue = r.MeasurementValues.Select(v => new MeasurementValueDto
                            {
                                Id = v.Id,
                                Value = v.Value,
                                TakenAt = v.TakenAt,
                                Note = v.Note,
                                MeasurementRequestId = v.MeasurementRequestId
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToListAsync();
        }
    }
}
