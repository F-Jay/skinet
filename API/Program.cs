using API.Extensions;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

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

builder.Services.AddDbContext<AppIdentityDbContext>(x => x.UseSqlite(configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddSingleton<IConnectionMultiplexer>(c => {
    var _config = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"),
    true);
    return ConnectionMultiplexer.Connect(_config);
});

builder.AddApplicationServices(); // Custom Extension
builder.Services.AddIdentityServices(configuration); // Custom Extension
builder.AddSwaggerDocumentation(); // Custom Extension

builder.Services.AddCors( opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
         policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
    });
});

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

        // Seed User Data into Identity DB.
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var identityContext = services.GetRequiredService<AppIdentityDbContext>();
        await identityContext.Database.MigrateAsync();
        await AppIdentityDbContextSeed.SeedUsersAsync(userManager);

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

app.UseCors("CorsPolicy");

app.UseAuthentication(); // UseAuthentication needs to be before - UseAuthorization
app.UseAuthorization();

app.UseSwaggerDocumentation(); // Custom Extension Middleware.

app.UseStaticFiles(); // Added to serve static files from the API - Product Images

app.MapControllers();

app.Run();
