using BeatZ.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BeatZ.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<BeatzDbContext>(name: "Application Database");

            services.AddDbContext<BeatzDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BeatZConnection"),   
            b => b.MigrationsAssembly("BeatZ.Api"))
                .LogTo(Console.WriteLine, LogLevel.Information)); // disable for production;

            services.AddScoped<IBeatzDbContext>(provider =>
                provider.GetService<BeatzDbContext>());
        }
    }
}
