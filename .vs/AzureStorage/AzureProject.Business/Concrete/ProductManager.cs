using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureProject.Business.Abstract.Services;
using AzureProject.Core.Repositories.AzureRepository;
using AzureProject.Entities.Entities.Azure;

namespace AzureProject.Business.Concrete
{
    public class ProductManager:IProductService
    {
        private readonly ITableStorage<Product> _azureProductRepository;
        public ProductManager(ITableStorage<Product> azureProductRepository)
        {
            _azureProductRepository = azureProductRepository;
        }

        public List<Product> GetProducts(double price)
        {
            var products = _azureProductRepository.GetAll().ToList();
            if (price != 0)
            {
                products = products.Where(p => p.Price > price).ToList();
            }
            return products;
        }

        public async Task<Product> AddProduct(Product product)
        {
            var addedProduct = await _azureProductRepository.AddAsync(product);
            return addedProduct;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var updatedProduct = await _azureProductRepository.UpdateAsync(product);
            return updatedProduct;
        }

        public void Delete(string rowKey, string partitionKey)
        {
            _azureProductRepository.DeleteAsync(partitionKey, rowKey);
        }

        public async Task<Product> GetProductByRowKeyAndPartitionKey(string rowKey, string partitionKey)
        {
            return await _azureProductRepository.GetAsync(partitionKey, rowKey);
        }

        public List<Product> GetProductsFilterByPrice(double price)
        {
            return _azureProductRepository.GetList(x=>x.Price > price).ToList();
        }
    }
}