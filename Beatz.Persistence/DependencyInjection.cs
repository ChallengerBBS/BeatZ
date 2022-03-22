using BeatZ.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace BeatZ.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<BeatzDbContext>(name: "Application Database");

            services.AddDbContext<BeatzDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BeatZ"),
                b => b.MigrationsAssembly(typeof(BeatzDbContext).Assembly.FullName))
                .LogTo(Console.WriteLine, LogLevel.Information)); // disable for production;

            services.AddScoped<IBeatzDbContext>(provider =>
                provider.GetService<BeatzDbContext>());

            return services;
        }
    }
}
