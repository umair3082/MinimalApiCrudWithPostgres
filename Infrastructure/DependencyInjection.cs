using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Microsoft.EntityFrameworkCore; // Make sure this is included
using System;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            services.AddDbContextPool<DiscountDbContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            return services;
        }
    }

}
