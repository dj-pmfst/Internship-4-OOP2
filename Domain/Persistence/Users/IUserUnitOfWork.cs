namespace Domain.Persistence.Users
{
    public interface IUserUnitOfWork
    {
        IUserRepository Repository { get; }
    }
}
