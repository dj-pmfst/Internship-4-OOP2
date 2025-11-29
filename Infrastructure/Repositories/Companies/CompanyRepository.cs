using Domain.Entities.Companies;
using Domain.Persistence.Companies;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Companies
{
    internal class CompanyRepository : Repository<Company, int>, ICompanyRepository
    {
        private readonly CompanyDbContext _context;
        private readonly IDapperManager _dapperManager;
        public CompanyRepository(CompanyDbContext context, IDapperManager dapperManager)
            : base(context)
        {
            _context = context;
            _dapperManager = dapperManager;
        }
        public async Task<Company> GetById(int id)
        {
            return await _context.Set<Company>().FindAsync(id) ?? null!;
        }

        public async Task<bool> IsNameUniqueAsync(string name, int? id = null)
        {
            var sql = @"SELECT COUNT(*) 
                FROM Companies 
                WHERE Name = @Name AND (@Id IS NULL OR Id <> @Id)";

            var count = await _dapperManager.QuerySingleAsync<int>(sql, new { Name = name, Id = id });

            return count == 0;
        }

    }
}
