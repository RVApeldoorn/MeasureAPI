using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MeasurementApi.Controllers;
using MeasurementApi.DTOs;
using MeasurementApi.Services.Interfaces;

public class PatientControllerTests
{
    private readonly Mock<IMeasurementService> _serviceMock;
    private readonly PatientController _controller;

    public PatientControllerTests()
    {
        _serviceMock = new Mock<IMeasurementService>();
        _controller = new PatientController(_serviceMock.Object);
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

        _serviceMock.Setup(s => s.SubmitMeasurement(1, dto)).Returns(Task.CompletedTask);

        var result = await _controller.SubmitMeasurements(1, dto);

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

        var result = await _controller.SubmitMeasurements(1, dto);

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

        var result = await _controller.SubmitMeasurements(1, dto);

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task SubmitMeasurements_ReturnsBadRequest_WhenDtoIsNull()
    {
        var result = await _controller.SubmitMeasurements(1, null!);

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

        _serviceMock.Setup(s => s.SubmitMeasurement(1, dto))
                    .ThrowsAsync(new Exception("Database error"));

        var result = await _controller.SubmitMeasurements(1, dto);

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task SubmitMeasurements_AllowsNegativePatientId_ByDefault()
    {
        var dto = new MeasurementSubmissionDto
        {
            sessionId = -5,
            Values = new List<MeasurementValueDto>
            {
                new MeasurementValueDto { Value = 1 },
                new MeasurementValueDto { Value = 2 }
            }
        };

        _serviceMock.Setup(s => s.SubmitMeasurement(-5, dto)).Returns(Task.CompletedTask);

        var result = await _controller.SubmitMeasurements(-5, dto);

        result.Should().BeOfType<OkObjectResult>();
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

        _serviceMock.Setup(s => s.SubmitMeasurement(1, dto)).Returns(Task.CompletedTask);

        var result = await _controller.SubmitMeasurements(1, dto);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task SubmitMeasurements_ReturnsOk_WhenValuesContainLargeNumbers()
    {
        var dto = new MeasurementSubmissionDto
        {
            Values = new List<MeasurementValueDto>
            {
                new MeasurementValueDto { Value = double.MaxValue },
                new MeasurementValueDto { Value = double.MinValue }
            }
        };

        _serviceMock.Setup(s => s.SubmitMeasurement(1, dto)).Returns(Task.CompletedTask);

        var result = await _controller.SubmitMeasurements(1, dto);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task SubmitMeasurements_ReturnsOk_WhenValuesContainNegativeNumbers()
    {
        var dto = new MeasurementSubmissionDto
        {
            Values = new List<MeasurementValueDto>
            {
                new MeasurementValueDto { Value = -1 },
                new MeasurementValueDto { Value = -2 }
            }
        };

        _serviceMock.Setup(s => s.SubmitMeasurement(1, dto)).Returns(Task.CompletedTask);

        var result = await _controller.SubmitMeasurements(1, dto);

        result.Should().BeOfType<OkObjectResult>();
    }
}
