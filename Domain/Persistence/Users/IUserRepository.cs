using Domain.Entities.Users;
using Domain.Persistence.Common;

namespace Domain.Persistence.Users
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User> GetById(int id);
    }
}
