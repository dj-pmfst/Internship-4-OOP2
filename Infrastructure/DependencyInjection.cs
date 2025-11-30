using Application.Common.Handlers.Users;
using Application.Common.Interfaces;
using Domain.Persistence.Companies;
using Domain.Persistence.Users;
using Infrastructure.Database;
using Infrastructure.Repositories.Companies;
using Infrastructure.Repositories.Users;
using Infrastructure.Services.Cache;
using Infrastructure.Services.External;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string? usersConnectionString = configuration.GetConnectionString("UsersDatabase");
            string? companiesConnectionString = configuration.GetConnectionString("CompaniesDatabase");

            if (string.IsNullOrEmpty(usersConnectionString))
                throw new ArgumentNullException(nameof(usersConnectionString));
            if (string.IsNullOrEmpty(companiesConnectionString))
                throw new ArgumentNullException(nameof(companiesConnectionString));

            services.AddDbContext<UserDbContext>(options =>
                options.UseNpgsql(usersConnectionString));

            services.AddDbContext<CompanyDbContext>(options =>
                options.UseNpgsql(companiesConnectionString));

 
            services.AddScoped<IUserRepository, UserRepository>(sp =>
            {
                var ctx = sp.GetRequiredService<UserDbContext>();
                var dapper = new DapperManager(usersConnectionString); 
                return new UserRepository(ctx, dapper);
            });

            services.AddScoped<ICompanyRepository, CompanyRepository>(sp =>
            {
                var ctx = sp.GetRequiredService<CompanyDbContext>();
                var dapper = new DapperManager(companiesConnectionString); 
                return new CompanyRepository(ctx, dapper);
            });

            services.AddScoped<IUserUnitOfWork>(sp =>
            {
                var dbContext = sp.GetRequiredService<UserDbContext>();
                var repo = sp.GetRequiredService<IUserRepository>();
                return new UserUnitOfWork(dbContext, repo);
            });

            services.AddScoped<ICompanyUnitOfWork>(sp =>
            {
                var dbContext = sp.GetRequiredService<CompanyDbContext>();
                var repo = sp.GetRequiredService<ICompanyRepository>();
                return new CompanyUnitOfWork(dbContext, repo);
            });

            services.AddScoped<DeactivateUserRequestHandler>();

            services.AddMemoryCache();

            services.AddSingleton<ICacheService, MemoryCacheService>();
            services.AddHttpClient<IExternalUserApiClient, ExternalUserApiClient>();

            services.AddScoped<ImportExternalUsersRequestHandler>();

            return services;
        }
    }
}