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
        public static IServiceProvider AddInfrastracture(this IServiceProvider serviceProvider, IConfiguration configuration)
        {
            AddDatabase(serviceProvider, configuration);
            return serviceProvider;
        }

        private static AddDatabase(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("Database");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            serviceProvider.AddDbContext<ApplicationDBContext>(options => options.UseNpgsql(connectionString));

            serviceProvider.AddScoped<IUnitOfWork, UnitOfWork>();

            serviceProvider.AddScoped<IUserRepository, UserRepository>();
            serviceProvider.AddScoped<IUserUnitOfWork, UserUnitOfWork>();

            serviceProvider.AddScoped<ICompanyRepository, CompanyRepository>();
            serviceProvider.AddScoped<ICompanyUnitOfWork, CompanyUnitOfWork>();

            serviceProvider.AddSingleton<IDapperManager>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                string cs = config.GetConnectionString("Database");
                return new DapperManager(cs);
            }); 
        }
    }
}
