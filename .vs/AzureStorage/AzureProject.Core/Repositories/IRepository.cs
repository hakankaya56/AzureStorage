using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AzureProject.Core.Repositories
{
    public interface IRepository<T> 
    {
        Task<T> AddAsync(T entity);
        T Add(T entity);
        T Update(T entity);
        Task<T> UpdateAsync(T entity);
    }
}