using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
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

        return app;
    }
}