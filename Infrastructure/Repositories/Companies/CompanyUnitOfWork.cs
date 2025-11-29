using Domain.Persistence.Companies;
using Infrastructure.Database;

namespace Infrastructure.Repositories.Companies
{
    public class CompanyUnitOfWork : ICompanyUnitOfWork
    {
        private readonly CompanyDbContext _dbContext;
        public ICompanyRepository Repository { get; }

        public CompanyUnitOfWork(CompanyDbContext dbContext, ICompanyRepository companyRepository)
        {
            _dbContext = dbContext;
            Repository = companyRepository;
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
