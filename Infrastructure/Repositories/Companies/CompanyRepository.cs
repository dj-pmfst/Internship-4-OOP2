using Domain.Entities.Companies;
using Domain.Persistence.Companies;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Companies
{
    internal class CompanyRepository : Repository<Company, int>, ICompanyRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly IDapperManager _dapperManager;
        public CompanyRepository(DbContext context, IDapperManager dapperManager)
            : base(context)
        {
            _dapperManager = dapperManager;
        }
        public async Task<Company> GetById(int id)
        {
            return await _applicationDBContext.Set<Company>().FindAsync(id) ?? null!;
        }

    }
}
