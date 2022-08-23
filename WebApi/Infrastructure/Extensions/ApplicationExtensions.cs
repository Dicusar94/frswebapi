using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Infrastructure.Extensions
{
    public static class ApplicationExtensions
    {
        public static void UseSwaggerService(this IApplicationBuilder app)
        {
            app.UseSwagger();

            var serviceProvider = app.ApplicationServices;
            var provider = serviceProvider.GetService<IApiVersionDescriptionProvider>();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider!.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        url: $"/swagger/{description.GroupName}/swagger.json",
                        name: description.GroupName.ToUpperInvariant());
                }
                options.RoutePrefix = string.Empty;
            });
        }
    }
}
