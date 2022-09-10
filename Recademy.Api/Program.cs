using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Recademy.Api.Extensions;
using Recademy.DataAccess;
using Recademy.DataAccess.Seeding;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ILogger logger = new LoggerConfiguration()
    .WriteTo.File("RecademyApi.log")
    .CreateLogger();

builder.Logging.AddSerilog(logger);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        name: "Recademy",
        new OpenApiInfo
        {
            Title = "Recademy API",
        });
});

builder.Services.AddRecademyAuthentication(builder.Configuration);

builder.Services.AddDbContext<RecademyContext>(options => options
    .UseInMemoryDatabase("RecademyDb")
    .UseLazyLoadingProxies());

builder.Services.AddScoped<IDbContextSeeder, DbContextSeeder>();
builder.Services.AddRecademyServices();

WebApplication app = builder.GetConfiguredRecademyApp();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Recademy.log")
    .CreateLogger();

app.Run();