using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MeasurementApi.Controllers;
using MeasurementApi.DTOs;
using MeasurementApi.Services.Interfaces;
using System.Security.Claims;

public class PatientControllerTests
{
    private readonly Mock<IMeasurementService> _serviceMock;
    private readonly PatientController _controller;

    private const string Patient1Id = "patient_1";
    private const string Patient2Id = "patient_2";

    public PatientControllerTests()
    {
        _serviceMock = new Mock<IMeasurementService>();
        _controller = new PatientController(_serviceMock.Object);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim("patient_id", Patient1Id)
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }

    [Fact]
    public async Task SubmitMeasurements_ReturnsOk_WhenValid()
    {
        var dto = new MeasurementSubmissionDto
        {
            sessionId = 1,
            Values = new List<MeasurementValueDto>
            {
                new MeasurementValueDto { Value = 1 },
                new MeasurementValueDto { Value = 2 }
            }
        };

        _serviceMock.Setup(s => s.SubmitMeasurement(Patient1Id, dto)).Returns(Task.CompletedTask);

        var result = await _controller.SubmitMeasurements(dto);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task SubmitMeasurements_ReturnsBadRequest_WhenNoValues()
    {
        var dto = new MeasurementSubmissionDto
        {
            sessionId = 1,
            Values = new List<MeasurementValueDto>()
        };

        var result = await _controller.SubmitMeasurements(dto);

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task SubmitMeasurements_ReturnsBadRequest_WhenValuesIsNull()
    {
        var dto = new MeasurementSubmissionDto
        {
            sessionId = 1,
            Values = null!
        };

        var result = await _controller.SubmitMeasurements(dto);

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task SubmitMeasurements_ReturnsBadRequest_WhenDtoIsNull()
    {
        var result = await _controller.SubmitMeasurements(null!);

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task SubmitMeasurements_ReturnsBadRequest_WhenServiceThrows()
    {
        var dto = new MeasurementSubmissionDto
        {
            sessionId = 1,
            Values = new List<MeasurementValueDto>
            {
                new MeasurementValueDto { Value = 1 },
                new MeasurementValueDto { Value = 2 }
            }
        };

        _serviceMock.Setup(s => s.SubmitMeasurement(Patient1Id, dto))
                    .ThrowsAsync(new Exception("Database error"));

        var result = await _controller.SubmitMeasurements(dto);

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task SubmitMeasurements_ReturnsOk_WhenValuesContainZero()
    {
        var dto = new MeasurementSubmissionDto
        {
            sessionId = 1,
            Values = new List<MeasurementValueDto>
            {
                new MeasurementValueDto { Value = 0 },
                new MeasurementValueDto { Value = 0 }
            }
        };

        _serviceMock.Setup(s => s.SubmitMeasurement(Patient1Id, dto)).Returns(Task.CompletedTask);

        var result = await _controller.SubmitMeasurements(dto);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task SubmitMeasurements_ReturnsOk_WhenValuesContainLargeNumbers()
    {
        var dto = new MeasurementSubmissionDto
        {
            sessionId = 1,
            Values = new List<MeasurementValueDto>
            {
                new MeasurementValueDto { Value = double.MaxValue },
                new MeasurementValueDto { Value = double.MinValue }
            }
        };

        _serviceMock.Setup(s => s.SubmitMeasurement(Patient1Id, dto)).Returns(Task.CompletedTask);

        var result = await _controller.SubmitMeasurements(dto);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task SubmitMeasurements_ReturnsOk_WhenValuesContainNegativeNumbers()
    {
        var dto = new MeasurementSubmissionDto
        {
            sessionId = 1,
            Values = new List<MeasurementValueDto>
            {
                new MeasurementValueDto { Value = -1 },
                new MeasurementValueDto { Value = -2 }
            }
        };

        _serviceMock.Setup(s => s.SubmitMeasurement(Patient1Id, dto)).Returns(Task.CompletedTask);

        var result = await _controller.SubmitMeasurements(dto);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task SubmitMeasurements_ReturnsBadRequest_WhenPatientIdIsMissing()
    {
        var dto = new MeasurementSubmissionDto
        {
            sessionId = 1,
            Values = new List<MeasurementValueDto>
            {
                new MeasurementValueDto { Value = 1 },
                new MeasurementValueDto { Value = 2 }
            }
        };

        // Remove the patient ID from the claims
        _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

        var result = await _controller.SubmitMeasurements(dto);

        result.Should().BeOfType<UnauthorizedObjectResult>();
    }

    [Fact]
    public async Task SubmitMeasurements_ReturnsBadRequest_WhenSessionIdIsInvalid()
    {
        var dto = new MeasurementSubmissionDto
        {
            sessionId = -1,
            Values = new List<MeasurementValueDto>
            {
                new MeasurementValueDto { Value = 1 },
                new MeasurementValueDto { Value = 2 }
            }
        };

        var result = await _controller.SubmitMeasurements(dto);

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task SubmitMeasurements_ReturnsBadRequest_WhenSessionIdIsNotFound()
    {
        var dto = new MeasurementSubmissionDto
        {
            sessionId = 999,
            Values = new List<MeasurementValueDto>
            {
                new MeasurementValueDto { Value = 1 },
                new MeasurementValueDto { Value = 2 }
            }
        };

        _serviceMock.Setup(s => s.SubmitMeasurement(Patient1Id, dto))
                    .ThrowsAsync(new Exception("Session not found"));

        var result = await _controller.SubmitMeasurements(dto);

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task GetSessions_ReturnsOk_WhenSessionsExist()
    {
        var sessions = new List<MeasurementSessionOverviewDto>
        {
            new MeasurementSessionOverviewDto
            {
                SessionId = 1,
                DueDate = DateTime.Now.AddDays(1),
                IsCompleted = false,
                Requests = new List<MeasurementRequestDto>()
            }
        };

        _serviceMock.Setup(s => s.GetSessionsByPatient(Patient1Id)).ReturnsAsync(sessions);

        var result = await _controller.GetSessions();

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetSessions_ReturnsNotFound_WhenNoSessionsExist()
    {
        _serviceMock.Setup(s => s.GetSessionsByPatient(Patient1Id)).ReturnsAsync(new List<MeasurementSessionOverviewDto>());

        var result = await _controller.GetSessions();

        result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task GetSessions_ReturnsUnauthorized_WhenPatientIdIsMissing()
    {
        // Remove the patient ID from the claims
        _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

        var result = await _controller.GetSessions();

        result.Should().BeOfType<UnauthorizedObjectResult>();
    }

    [Fact]
    public async Task GetSessions_ReturnsBadRequest_WhenServiceThrows()
    {
        _serviceMock.Setup(s => s.GetSessionsByPatient(Patient1Id))
                    .ThrowsAsync(new Exception("Database error"));

        var result = await _controller.GetSessions();

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task GetSessions_ReturnsOk_WhenSessionsContainNullValues()
    {
        var sessions = new List<MeasurementSessionOverviewDto>
        {
            new MeasurementSessionOverviewDto
            {
                SessionId = 1,
                DueDate = DateTime.Now.AddDays(1),
                IsCompleted = false,
                Requests = null
            }
        };

        _serviceMock.Setup(s => s.GetSessionsByPatient(Patient1Id)).ReturnsAsync(sessions);

        var result = await _controller.GetSessions();

        result.Should().BeOfType<OkObjectResult>();
    }
}