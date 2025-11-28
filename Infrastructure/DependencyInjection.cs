using Domain.Persistence.Common;
using Domain.Persistence.Companies;
using Domain.Persistence.Users;
using Infrastructure.Database;
using Infrastructure.Repositories.Companies;
using Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        // Correct: IServiceCollection
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDatabase(services, configuration);
            return services;
        }

        private static void AddDatabase(IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("Database");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            services.AddDbContext<ApplicationDBContext>(options => options.UseNpgsql(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyUnitOfWork, CompanyUnitOfWork>();

            services.AddSingleton<IDapperManager>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                string cs = config.GetConnectionString("Database");
                return new DapperManager(cs);
            }); 
        }
    }
}
