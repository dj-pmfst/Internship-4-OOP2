using Domain.Persistence.Companies;
using Infrastructure.Database;

namespace Infrastructure.Repositories.Companies
{
    internal class CompanyUnitOfWork : ICompanyUnitOfWork
    {
        private readonly ApplicationDBContext _dbContext;
        public ICompanyRepository CompanyRepository { get; set; }

        public CompanyUnitOfWork(ApplicationDBContext dbContext, ICompanyRepository companyRepository)
        {
            _dbContext = dbContext;
            CompanyRepository = companyRepository;
        }
    }
}
