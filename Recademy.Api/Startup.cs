using System;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Recademy.Api.Repositories;
using Recademy.Api.Repositories.Implementations;
using Recademy.Api.Services.Abstraction;
using Recademy.Api.Services.Implementations;
using Recademy.Api.Tools;
using Serilog;

namespace Recademy.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(configuration =>
            {
                configuration.SwaggerDoc("Recademy", new OpenApiInfo
                {
                    Title = "Recademy API",
                    Version = "0.2.0"
                });
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                configuration.IncludeXmlComments(xmlPath);
            });

            services
                .AddAuthentication(options =>
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
                    options.ClientId = _configuration["OAuth:GitHub:ClientId"];
                    options.ClientSecret = _configuration["OAuth:GitHub:ClientSecret"];

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


            //services.AddDbContext<RecademyContext>(options => options.UseSqlServer(_configuration["connectionString:RecademyDB"]));
            services.AddDbContext<RecademyContext>(options => options.UseInMemoryDatabase("RecademyDb"));

            services.AddScoped<IOauthProviderService, OauthProviderService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IGithubApiAccessor, GithubApiAccessor>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IGamificationService, GamificationService>();
            services.AddScoped<IGithubService, GithubService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAchievementService, AchievementService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Recademy.log")
                .CreateLogger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(configuration =>
                configuration.SwaggerEndpoint("/swagger/Recademy/swagger.json", "Recademy API"));
        }
    }
}