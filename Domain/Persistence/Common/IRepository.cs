using Domain.Common.Model;

namespace Domain.Persistence.Common
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        Task<GetAllResponse<TEntity>> GetAll();
        Task<TEntity?> GetByIdAsync(TId id);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        void Delete(TEntity? entity); 
        Task DeleteByIdAsync(TId id);
    }
}
