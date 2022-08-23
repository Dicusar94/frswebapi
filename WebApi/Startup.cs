using Bussines.Abstraction.Authorization.Interfaces;
using Bussines.Services.Authorization.Implementations;
using Data.MoackData;
using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Infrastructure.Configurations;
using WebApi.Infrastructure.Extensions;
using WebApi.Infrastructure.Middlewares;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var authOptions = services.ConfigureAuthOptions(Configuration);
            services.AddJwtAuthentication(authOptions);

            services.AddApiVersioningService();

            services.AddRouting(opt => opt.LowercaseUrls = true);
            services.AddControllers();

            services.AddSwaggerGenService();
            services.ConfigureOptions<ConfigureSwaggerOptions>();

            services.AddSingleton<IDataBase, DataBase>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseSwaggerService();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}