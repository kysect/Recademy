using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.LoginPath = "/api/auth/signIn";
                    options.LogoutPath = "/api/auth/signOut";
                })
                .AddGitHub(options =>
                {
                    options.ClientId = GhUtil.AppClientId;
                    options.ClientSecret = GhUtil.AppSecret;
                    options.Scope.Add("user:email");
                    options.Scope.Add("read:user");
                });


            services.AddDbContext<RecademyContext>(options =>
                options.UseSqlServer(Configuration["connectionString:RecademyDB"]));

            _logFilePath = Configuration["LogFilePath"];

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
            loggerFactory.AddFile(_logFilePath);

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