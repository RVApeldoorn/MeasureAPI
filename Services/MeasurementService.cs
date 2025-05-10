using MeasurementApi.Data;
using MeasurementApi.DTOs;
using MeasurementApi.Models;
using MeasurementApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeasurementApi.Services;

public class MeasurementService : IMeasurementService
{
    private readonly AppDbContext _db;

    public MeasurementService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<int> CreateMeasurementSession(CreateMeasurementSessionDto dto)
    {
        var session = new MeasurementSession
        {
            CreatedByUserId = dto.CreatedByUserId,
            PatientId = dto.PatientId,
            DueDate = dto.DueDate,
        };

        session.MeasurementRequests = dto.Requests.Select(requestDto => new MeasurementRequest
        {
            MeasurementSessionId = session.Id,
            MeasurementTypeId = requestDto.MeasurementTypeId
        }).ToList();

        _db.MeasurementSessions.Add(session);
        await _db.SaveChangesAsync();

        return session.Id;
    }

    public async Task<PatientSessionsOverviewDto> GetSessionsByPatient(string patientId)
    {
        var sessions = await _db.MeasurementSessions
            .Where(s => s.PatientId == patientId)
            .Include(s => s.MeasurementRequests)
                .ThenInclude(r => r.MeasurementType)
            .Include(s => s.MeasurementRequests)
                .ThenInclude(r => r.MeasurementValues)
            .Include(s => s.Patient)
            .ToListAsync();

        string patientName = "";

        if (sessions.Any())
        {
            patientName = sessions.First().Patient?.Name ?? "";
        }
        else
        {
            var patient = await _db.Patients.FindAsync(patientId);
            patientName = patient?.Name ?? "";
        }

        var sessionDtos = sessions.Select(s => new MeasurementSessionOverviewDto
        {
            SessionId = s.Id,
            DueDate = s.DueDate,
            IsCompleted = s.IsCompleted,
            Requests = s.MeasurementRequests.Select(r => new MeasurementRequestDto
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
                    Note = v.Note
                }).ToList()
            }).ToList()
        }).ToList();

        return new PatientSessionsOverviewDto
        {
            PatientName = patientName,
            Sessions = sessionDtos
        };
    }

    public async Task SubmitMeasurement(string PatientId, MeasurementSubmissionDto dto)
    {
        foreach (var valueDto in dto.Values)
        {
            var existingValue = await _db.MeasurementValues
                .FirstOrDefaultAsync(v => v.MeasurementRequestId == valueDto.MeasurementRequestId);

            if (existingValue != null)
            {
                continue;
            }

            var value = new MeasurementValue
            {
                MeasurementRequestId = valueDto.MeasurementRequestId,
                Value = valueDto.Value,
                TakenAt = valueDto.TakenAt,
                Note = valueDto.Note
            };

            _db.MeasurementValues.Add(value);

            var request = await _db.MeasurementRequests
                .Include(r => r.MeasurementValues)
                .Include(r => r.MeasurementSession)
                .FirstOrDefaultAsync(r => r.Id == valueDto.MeasurementRequestId);

            if (request != null && request.MeasurementSession != null && !request.MeasurementSession.IsCompleted)
            {
                var allRequests = await _db.MeasurementRequests
                    .Where(r => r.MeasurementSessionId == request.MeasurementSessionId)
                    .Include(r => r.MeasurementValues)
                    .ToListAsync();

                if (allRequests.All(r => r.MeasurementValues.Any()))
                {
                    request.MeasurementSession.IsCompleted = true;
                }
            }
        }

        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<MeasurementTypeDto>> GetAllMeasurementTypes()
    {
        return await _db.MeasurementTypes
            .Select(mt => new MeasurementTypeDto
            {
                Id = mt.Id,
                Name = mt.Name,
                Unit = mt.Unit
            }).ToListAsync();
    }
}
