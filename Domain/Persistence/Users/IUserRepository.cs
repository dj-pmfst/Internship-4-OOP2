using Domain.Entities.Users;

namespace Domain.Persistence.Users
{
    internal interface IUserRepository
    {
        Task<User> GetById(int id);
    }
}
