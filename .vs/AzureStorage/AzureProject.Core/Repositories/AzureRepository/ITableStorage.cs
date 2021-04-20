using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AzureProject.Core.Repositories.AzureRepository
{
    public interface ITableStorage<TEntity>
    {
        Task<TEntity> GetAsync(string partitionKey, string rowKey);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null);
        Task DeleteAsync(string partitionKey, string rowKey);
        Task<TEntity> AddAsync(TEntity entity);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}