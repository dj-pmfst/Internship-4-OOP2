namespace Domain.Persistence.Common
{
    internal interface IUnitOfWork
    {
        Task CreateTransaction();
        Task Commit();
        Task Rollback();
        Task SaveAsync();
    }
}
