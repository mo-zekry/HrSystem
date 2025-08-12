using HrSystem.Application;
using HrSystem.Infrastructure;
using HrSystem.Infrastructure.Persistence;
using HrSystem.Infrastructure.Persistence.Seed;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "HR System API",
            Version = "v1",
            Description =
                "API for managing HR system entities like employees, org units, and org types.",
        }
    );
    options.EnableAnnotations();
});

// Application
builder.Services.AddApplication();

// Infrastructure
builder.Services.AddInfrastructure(options =>
{
    options
        .UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql"))
        .EnableDetailedErrors()
        .EnableSensitiveDataLogging();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// Global exception handler and HSTS/HTTPS in non-dev
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
    app.UseHttpsRedirection();
}

// Use Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HR System API V1");
        c.RoutePrefix = string.Empty; // Serve Swagger at root
    });
}

app.UseAuthorization();

app.MapControllers();

// Database migrate + seed (Development only)
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<HrSystemDbContext>();
    var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
    var logger = loggerFactory.CreateLogger("DbSeeder");
    await DbSeeder.SeedAsync(db, logger);
}

app.Run();
