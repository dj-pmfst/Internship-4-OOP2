using Domain.Persistence.Companies;
using Infrastructure.Database;

namespace Infrastructure.Repositories.Companies
{
    public class CompanyUnitOfWork : ICompanyUnitOfWork
    {
        private readonly ApplicationDBContext _dbContext;
        public ICompanyRepository Repository { get; }

        public CompanyUnitOfWork(ApplicationDBContext dbContext, ICompanyRepository companyRepository)
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
