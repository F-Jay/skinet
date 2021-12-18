using API.Extensions;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add Connection String as Service - SQLlite
IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.Development.json")
            .Build();
builder.Services.AddDbContext<StoreContext>(x => x.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

builder.AddApplicationServices(); // Custom Extension
builder.AddSwaggerDocumentation(); // Custom Extension

// Add Automapper as a Service.
builder.Services.AddAutoMapper(typeof(MappingProfiles));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync(); // Create Database If not already exists, and apply any pending migrations to the database.
        await StoreContextSeed.SeedAsync(context, loggerFactory); // Seed Data Into Database.
    }catch(Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred during migration - Logged in Program.cs");
    }
}


// Configure the HTTP request pipeline. - Ordering is IMPORTANT!

app.UseMiddleware<ExceptionMiddleware>(); // Use customer exception middle ware.

app.UseStatusCodePagesWithReExecute("/errors/{}");

app.UseHttpsRedirection(); // If used Http url - Will automatical redirect to Https urls

app.UseAuthorization();

app.UseSwaggerDocumentation(); // Custom Extension Middleware.

app.UseStaticFiles(); // Added to serve static files from the API - Product Images

app.MapControllers();

app.Run();
