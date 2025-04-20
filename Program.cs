using MeasurementApi.Data;
using Microsoft.EntityFrameworkCore;
using MeasurementApi.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using MeasurementApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=measurements.db"));

builder.Services.AddTransient<IMeasurementService, MeasurementService>();
builder.Services.AddTransient<IPatientService, PatientService>();

builder.Services.Configure<BearerTokenOptions>(builder.Configuration.GetSection("BearerTokenOptions"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    await DbSeeder.SeedAsync(db);
}

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    
app.Run();

public class BearerTokenFilter : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var BearerTokenOption = context.HttpContext.RequestServices.GetService<IOptions<BearerTokenOptions>>()!.Value;
        if (!context.HttpContext.Request.Headers.ContainsKey("BearerToken")){
            context.HttpContext.Response.StatusCode = 401;
            return;
        }
        if (context.HttpContext.Request.Headers["BearerToken"] != BearerTokenOption.BearerToken){
            context.HttpContext.Response.StatusCode = 401;
            return;
        }
        await next();
    }
}

public class BearerTokenOptions {
    public string BearerToken  {get; set;} = "";
}