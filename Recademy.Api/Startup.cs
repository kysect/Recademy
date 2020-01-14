using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Recademy.Api.Services;
using Recademy.Api.Services.Abstraction;

namespace Recademy.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(configuration =>
            {
                configuration.SwaggerDoc("Recademy", new OpenApiInfo
                {
                    Title = "Recademy API",
                    Version = "0.0.1"
                });
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                configuration.IncludeXmlComments(xmlPath);
            });
            services.AddDbContext<RecademyContext>(options =>
                options.UseSqlServer(Configuration["connectionString:RecademyDB"]));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IGameficationService, GameficationService>();
            services.AddScoped<IGithubService, GithubService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAchievementService, AchievementService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(configuration =>
                configuration.SwaggerEndpoint("/swagger/Recademy/swagger.json", "Recademy API"));
        }
    }
}