using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureProject.Business.Abstract.Services;
using AzureProject.Entities.Entities.Azure;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebUI.Models.Products;

namespace NetCoreWebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(double price = 0)
        {
            var products = _productService.GetProducts(price);
            var productViewModel = new ProductViewModel
            {
                ProductList = products
            };
            return View(productViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            product.RowKey = Guid.NewGuid().ToString();
            product.PartitionKey = "Test";
            await _productService.AddProduct(product);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(string rowKey, string partitionKey)
        {
          var product = await _productService.GetProductByRowKeyAndPartitionKey(rowKey, partitionKey);
          var productViewModel = new ProductViewModel
          {
              Product = product
          };
          return View(productViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product product)
        {
            await _productService.UpdateProduct(product);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string rowKey, string partitionKey)
        {
            _productService.Delete(rowKey,partitionKey);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetProductFilterByPrice(double price)
        {
            return RedirectToAction("Index", new {price = price});
        }
    }
}
