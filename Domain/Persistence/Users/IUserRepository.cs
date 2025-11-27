using Domain.Entities.Users;

namespace Domain.Persistence.Users
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);
    }
}
