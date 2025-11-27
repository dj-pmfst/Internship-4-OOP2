using Domain.Persistence.Users;
using Infrastructure.Database;

namespace Infrastructure.Repositories.Users
{
    internal class UserUnitOfWork : IUserUnitOfWork
    {
        private readonly ApplicationDBContext _dbContext;
        public IUserRepository UserRepository { get; set; }

        public UserUnitOfWork(ApplicationDBContext dbContext, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            UserRepository = userRepository;
        }
    }
}
