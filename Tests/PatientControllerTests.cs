using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MeasurementApi.Controllers;
using MeasurementApi.DTOs;
using MeasurementApi.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

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
}