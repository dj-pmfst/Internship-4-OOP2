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

        public async Task<bool> IsNameUniqueAsync(string name, int? id = null)
        {
            var sql = @"SELECT COUNT(*) 
                FROM Companies 
                WHERE Name = @Name AND (@Id IS NULL OR Id <> @Id)";

            var count = await _dapper.QuerySingleAsync<int>(sql, new { Name = name, Id = id });

            return count == 0;
        }

    }
}
