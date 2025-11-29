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
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            string? connectionString = configuration.GetConnectionString("Database");
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            services.AddDbContext<ApplicationDBContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddSingleton<IDapperManager>(sp => new DapperManager(connectionString));

            services.AddScoped<IUserRepository, UserRepository>(sp =>
            {
                var ctx = sp.GetRequiredService<ApplicationDBContext>();
                var dapper = sp.GetRequiredService<IDapperManager>();
                return new UserRepository(ctx, dapper);
            });

            services.AddScoped<ICompanyRepository, CompanyRepository>(sp =>
            {
                var ctx = sp.GetRequiredService<ApplicationDBContext>();
                var dapper = sp.GetRequiredService<IDapperManager>();
                return new CompanyRepository(ctx, dapper);
            });

            services.AddScoped<IUserUnitOfWork>(sp =>
            {
                var dbContext = sp.GetRequiredService<ApplicationDBContext>();
                var repo = sp.GetRequiredService<IUserRepository>();
                return new UserUnitOfWork(dbContext, repo);
            });

            services.AddScoped<ICompanyUnitOfWork>(sp =>
            {
                var dbContext = sp.GetRequiredService<ApplicationDBContext>();
                var repo = sp.GetRequiredService<ICompanyRepository>();
                return new CompanyUnitOfWork(dbContext, repo);
            });

            services.AddSingleton<ICacheService, MemoryCacheService>();

            services.AddHttpClient<IExternalUserApiClient, ExternalUserApiClient>();

            return services;
        }
    }
}
