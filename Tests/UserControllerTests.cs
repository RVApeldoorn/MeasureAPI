using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MeasurementApi.Controllers;
using MeasurementApi.DTOs;
using MeasurementApi.Services.Interfaces;

public class UserControllerTests
{
    private readonly Mock<IPatientService> _patientServiceMock;
    private readonly Mock<IMeasurementService> _measurementServiceMock;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _patientServiceMock = new Mock<IPatientService>();
        _measurementServiceMock = new Mock<IMeasurementService>();
        _controller = new UserController(_patientServiceMock.Object, _measurementServiceMock.Object);
    }

    [Fact]
    public async Task GetAllPatients_ReturnsOk_WhenPatientsExist()
    {
        var patients = new List<PatientDto> { new PatientDto { Id = "1", Name = "Alice" } };
        _patientServiceMock.Setup(s => s.GetAllPatients()).ReturnsAsync(patients);

        var result = await _controller.GetAllPatients();

        result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeEquivalentTo(patients);
    }

    [Fact]
    public async Task GetAllPatients_ReturnsNotFound_WhenNoPatientsFound()
    {
        _patientServiceMock.Setup(s => s.GetAllPatients()).ReturnsAsync(new List<PatientDto>());

        var result = await _controller.GetAllPatients();

        result.Should().BeOfType<NotFoundObjectResult>()
            .Which.Value.Should().Be("No patients found.");
    }

    [Fact]
    public async Task CreateMeasurementSession_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        _controller.ModelState.AddModelError("PatientId", "Required");

        var result = await _controller.CreateMeasurementSession(new CreateMeasurementSessionDto());

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task CreateMeasurementSession_ReturnsBadRequest_WhenServiceFails()
    {
        var dto = new CreateMeasurementSessionDto { PatientId = "1" };
        _measurementServiceMock.Setup(m => m.CreateMeasurementSession(dto)).ReturnsAsync(-1);

        var result = await _controller.CreateMeasurementSession(dto);

        result.Should().BeOfType<BadRequestObjectResult>()
            .Which.Value.Should().Be("Failed to create measurement session. Please check the provided data.");
    }

    [Fact]
    public async Task CreateMeasurementSession_ReturnsOk_WhenSessionIsCreated()
    {
        var dto = new CreateMeasurementSessionDto { PatientId = "1" };
        var sessionOverviewList = new List<MeasurementSessionOverviewDto>
        {
            new MeasurementSessionOverviewDto { SessionId = 123, DueDate = DateTime.UtcNow.AddDays(1), IsCompleted = false }
        };
        var overview = new PatientSessionsOverviewDto
        {
            Sessions = sessionOverviewList
        };

        _measurementServiceMock.Setup(m => m.CreateMeasurementSession(dto)).ReturnsAsync(123);
        _measurementServiceMock.Setup(m => m.GetSessionsByPatient("1")).ReturnsAsync(overview);

        var result = await _controller.CreateMeasurementSession(dto);

        result.Should().BeOfType<OkObjectResult>();

        dynamic response = ((OkObjectResult)result).Value!;
        Assert.Equal("Measurement session created.", response.message);
    }

    [Fact]
    public async Task CreateMeasurementSession_ReturnsBadRequest_WhenSessionCreationFails()
    {
        var dto = new CreateMeasurementSessionDto { PatientId = "1" };
        _measurementServiceMock.Setup(m => m.CreateMeasurementSession(dto)).ReturnsAsync(-1);

        var result = await _controller.CreateMeasurementSession(dto);

        result.Should().BeOfType<BadRequestObjectResult>()
            .Which.Value.Should().Be("Failed to create measurement session. Please check the provided data.");
    }
}
