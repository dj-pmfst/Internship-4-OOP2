using Domain.Persistence.Users;
using Infrastructure.Database;

namespace Infrastructure.Repositories.Users
{
    public class UserUnitOfWork : IUserUnitOfWork
    {
        private readonly ApplicationDBContext _dbContext;
        public IUserRepository Repository { get; }

        public UserUnitOfWork(ApplicationDBContext dbContext, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            Repository = userRepository;
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
