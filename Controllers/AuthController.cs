using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MeasurementApi.Data;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("setup")]
    public async Task<IActionResult> Setup([FromBody] SetupRequest request)
    {
        var setupCode = await _context.SetupCodes
            .FirstOrDefaultAsync(c => c.Code == request.SetupCode && !c.Used && c.ExpiresAt > DateTime.UtcNow);

        if (setupCode == null)
            return Unauthorized(new { message = "Invalid or expired setup code." });

        setupCode.Used = true;
        await _context.SaveChangesAsync();

        var token = GenerateJwtToken(setupCode.PatientId);
        return Ok(new { token });
    }

    private string GenerateJwtToken(string patientId)
    {
        var claims = new[]
        {
            new Claim("patient_id", patientId)
        };

        var jwtKey = _configuration["Jwt:Key"] ?? throw new Exception("JWT Key not configured.");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiresInMinutes"] ?? "60")),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class SetupRequest
{
    public string? SetupCode { get; set; }
}
