using Microsoft.AspNetCore.Builder;

namespace CourtApp.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "CourtApp.Api");
                options.RoutePrefix = "swagger";
                options.DisplayRequestDuration();
            });
        }
    }
}