using Domain.Entities.Users;
using Domain.Persistence.Common;

namespace Domain.Persistence.Users
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User> GetById(int id);
        Task<bool> IsUsernameUniqueAsync(string username, int? ignoreUserId = null);
        Task<bool> IsEmailUniqueAsync(string email, int? ignoreUserId = null);
    }
}
