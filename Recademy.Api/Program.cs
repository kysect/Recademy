using System;
using System.IO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Recademy.Api;
using Recademy.Api.Repositories;
using Recademy.Api.Repositories.Implementations;
using Recademy.Api.Services.Abstraction;
using Recademy.Api.Services.Implementations;
using Recademy.Api.Tools;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", policyConfig =>
{
    policyConfig
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
}));

var logger = new LoggerConfiguration()
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

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/api/auth/sign-in";
    options.LogoutPath = "/api/auth/sign-out";
})
.AddGitHub(options =>
{
    options.ClientId = builder.Configuration["OAuth:GitHub:ClientId"];
    options.ClientSecret = builder.Configuration["OAuth:GitHub:ClientSecret"];

    options.Scope.Add("read:user");
    options.Scope.Add("user:email");

    options.Events.OnCreatingTicket += context =>
    {
        if (context.AccessToken is { })
        {
            context.Identity?.AddClaim(new Claim("access_token", context.AccessToken));
        }

        return Task.CompletedTask;
    };

    options.CorrelationCookie.SameSite = SameSiteMode.Lax;
});

builder.Services.AddDbContext<RecademyContext>(options => options.UseInMemoryDatabase("RecademyDb"));

builder.Services.AddScoped<IOauthProviderService, OauthProviderService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IGithubApiAccessor, GithubApiAccessor>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGamificationService, GamificationService>();
builder.Services.AddScoped<IGithubService, GithubService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAchievementService, AchievementService>();

var app = builder.Build();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Recademy.log")
    .CreateLogger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.UseSwagger();
app.UseSwaggerUI(configuration => configuration.SwaggerEndpoint("/swagger/Recademy/swagger.json", "Recademy API"));

app.Run();
