using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Recademy.Api.Services;
using Recademy.Api.Services.Abstraction;
using Recademy.Api.Tools;

namespace Recademy.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private string _logFilePath;

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

            services.AddDbContext<RecademyContext>(options =>
                options.UseSqlServer(Configuration["connectionString:RecademyDB"]));

            _logFilePath = Configuration["LogFilePath"];

            services.AddScoped<IGithubApiAccessor, GithubApiAccessor>();
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
            loggerFactory.AddFile(_logFilePath);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

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