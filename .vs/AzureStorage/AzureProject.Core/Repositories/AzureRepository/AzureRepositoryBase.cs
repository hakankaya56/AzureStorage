using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AzureProject.Core.Repositories.AzureRepository
{
    public class AzureRepositoryBase<TEntity> : ITableStorage<TEntity> where TEntity : TableEntity, new()
    {
        private readonly CloudTableClient _cloudTableClient;  // Connection for all  tables //
        private readonly CloudTable _cloudTable; //connection for only one table//
        public AzureRepositoryBase()
        {
            var storageAccount = CloudStorageAccount.Parse(ConnectionStrings.AzureConnectionString);
            _cloudTableClient = storageAccount.CreateCloudTableClient();
            _cloudTable = _cloudTableClient.GetTableReference(typeof(TEntity).Name);
            _cloudTable.CreateIfNotExists();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var addOperation = TableOperation.InsertOrMerge(entity);
            var execute = await _cloudTable.ExecuteAsync(addOperation);
            return execute.Result as TEntity;
        }

        public TEntity Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var updateOperation = TableOperation.Replace(entity);
            var execute = await _cloudTable.ExecuteAsync(updateOperation);
            return execute.Result as TEntity;

        }

        public async Task<TEntity> GetAsync(string partitionKey, string rowKey)
        {
            var getOperation = TableOperation.Retrieve<TEntity>(partitionKey, rowKey);
            var execute = await _cloudTable.ExecuteAsync(getOperation);
            return execute.Result as TEntity;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _cloudTable.CreateQuery<TEntity>().AsQueryable();
        }

        public IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> filter =null)
        {
            return filter == null
                ? _cloudTable.CreateQuery<TEntity>().AsQueryable()
                : _cloudTable.CreateQuery<TEntity>().Where(filter);
        }

        public async Task DeleteAsync(string partitionKey, string rowKey)
        {
            var entity = await GetAsync(partitionKey, rowKey);
            var deleteOperation = TableOperation.Delete(entity);
            await _cloudTable.ExecuteAsync(deleteOperation);
        }
    }
}