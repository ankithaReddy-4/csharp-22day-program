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
app.MapGet("/api/patients",
    (CareBridgeScaffoldContext db, string? city,string? name) =>
    {
        var query = db.Patients.AsQueryable();

        // Apply filters if parameters are provided
        if (!string.IsNullOrEmpty(city))
        {
            query = query.Where(p => p.City == city);
        }
        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(p => p.FullName.Contains(name));
        }
        
            query = query.Where(p => p.IsActive);
        

        return query
                 .Select(p => new
                 {
                     p.PatientId,
                     p.FullName,
                     p.City,
                     p.IsActive
                 })
                 .OrderBy(p => p.FullName)
                 .Take(20)
                 .ToList();
    });

app.Run();
