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

            return await _dapperManager.QuerySingleAsync<User>(sql, paramaters)
        }
    }
}
