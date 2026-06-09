using CareBridge.EFCoreDemo.Models.Generated;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register EF Core DbContext.
// ASP.NET Core will automatically create and inject it when needed.
builder.Services.AddDbContext<CareBridgeScaffoldContext>();

// Add Swagger support.
// Swagger gives us a testing screen for APIs.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow Vue.js running on another port
// to call this API from the browser.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Enable Swagger.
app.UseSwagger();
app.UseSwaggerUI();

// Enable CORS.
app.UseCors();

// Simple health-check endpoint.
app.MapGet("/", () =>
{
    return "CareBridge API is running";
});

// Return first 20 patients.
// EF Core converts this LINQ query into SQL.
app.MapGet("/api/analytics/department-load",
    (CareBridgeScaffoldContext db, int days) =>
    {
        if (days <= 0) days = 60;

        var cutoffDate = DateTime.UtcNow.AddDays(-days);
        var result = db.Encounters
            .Join(db.Departments,
                  e => e.DepartmentId,
                  d => d.DepartmentId,
                  (e, d) => new { e, d })
            .Where(x => x.e.AdmitDate >= cutoffDate)
            .GroupBy(x => x.d.Name)
            .Select(g => new
            {
                DepartmentName = g.Key,
                Inpatient = g.Count(x => x.e.EncounterType == "Inpatient"),
                Outpatient = g.Count(x => x.e.EncounterType == "Outpatient"),
                ED = g.Count(x => x.e.EncounterType == "ED"),
                Total = g.Count()
                
            })
            .OrderByDescending(r => r.Total)
            .ToList();

        var grandTotal = new
        {
            DepartmentName = "Grand Total",
            Inpatient = result.Sum(r => r.Inpatient),
            Outpatient = result.Sum(r => r.Outpatient),
            ED = result.Sum(r => r.ED),
            Total = result.Sum(r => r.Total)
        };
        result.Add(grandTotal);

        return result;
    });

app.Run();
