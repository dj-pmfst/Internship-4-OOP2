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
        //public Task<User> GetById(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
