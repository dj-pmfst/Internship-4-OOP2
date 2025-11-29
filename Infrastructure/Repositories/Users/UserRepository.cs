using Domain.Entities.Users;
using Domain.Persistence.Users;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users
{
    public sealed class UserRepository : Repository<User, int>, IUserRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly IDapperManager _dapperManager;
        public UserRepository(DbContext context, IDapperManager dapperManager) 
            : base(context)
        {
            _dapperManager = dapperManager;
        }
        public async Task<User> GetById(int id)
        {
            var sql =
                """
                SELECT
                    id AS Id 
                    name AS Name //fale ostala svojstva oml
                FROM
                    public.users
                WHERE
                    id=@Id 
                """;
            var paramaters = new
            {
                Id = id,
            };

            return await _dapperManager.QuerySingleAsync<User>(sql, paramaters);
        }

        public async Task<bool> IsUsernameUniqueAsync(string name, int? id = null)
        {
            var sql = @"SELECT COUNT(*) 
                FROM Users 
                WHERE Username = @Username AND (@Id IS NULL OR Id <> @Id)";

            var count = await _dapperManager.QuerySingleAsync<int>(sql, new { Name = name, Id = id });

            return count == 0;
        }
        public async Task<bool> IsEmailUniqueAsync(string name, int? id = null)
        {
            var sql = @"SELECT COUNT(*) 
                FROM Users 
                WHERE Email = @Email AND (@Id IS NULL OR Id <> @Id)";

            var count = await _dapperManager.QuerySingleAsync<int>(sql, new { Name = name, Id = id });

            return count == 0;
        }

    }
}
