using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Recademy.DataAccess;
using System;

namespace Recademy.Api.Extensions;

public static class RecademyAppConfigurationExtensions
{
    public static WebApplication GetConfiguredRecademyApp(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        WebApplication app = builder.Build();

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

        using IServiceScope scope = app.Services.CreateScope();
        RecademyContext context = scope.ServiceProvider.GetRequiredService<RecademyContext>();
        context.Database.EnsureCreated();

        return app;
    }
}