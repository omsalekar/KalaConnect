using KalaConnect.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database configuration
builder.Services.AddDbContext<KalaConnectDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("ReactPolicy");

// Health endpoint
app.MapGet("/api/health", () =>
{
    return Results.Ok(new
    {
        Application = "KalaConnect API",
        Status = "Running",
        Version = "1.0"
    });
});

// Database connectivity endpoint
app.MapGet("/api/db-check", async (
    KalaConnectDbContext dbContext) =>
{
    var isConnected = await dbContext.Database.CanConnectAsync();

    return Results.Ok(new
    {
        Database = "KalaConnectDb",
        Connected = isConnected
    });
});

app.Run();