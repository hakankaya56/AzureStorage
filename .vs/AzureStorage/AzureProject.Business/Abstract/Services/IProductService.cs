using System.Collections.Generic;
using System.Threading.Tasks;
using AzureProject.Entities.Entities.Azure;

namespace AzureProject.Business.Abstract.Services
{
    public interface IProductService
    {
        List<Product> GetProducts(double price);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        void Delete(string rowKey, string partitionKey);
        Task<Product> GetProductByRowKeyAndPartitionKey(string rowKey, string partitionKey);
        List<Product> GetProductsFilterByPrice(double price);
    }
}