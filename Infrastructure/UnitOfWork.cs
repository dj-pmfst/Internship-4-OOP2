using Domain.Persistence.Common;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _applicationDBContext;
        private IDbContextTransaction _transaction;

        public UnitOfWork(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task Commit()
        {
            if ( _transaction != null )
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
            }
        }

        public async Task CreateTransaction()
        {
            _transaction = await _applicationDBContext.BeginTransactionAsync();
        }

        public async Task Rollback()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _applicationDBContext.SaveChangesAsync();
        }
    }
}
