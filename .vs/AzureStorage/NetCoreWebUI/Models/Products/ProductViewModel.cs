using System.Collections.Generic;
using AzureProject.Entities.Entities.Azure;

namespace NetCoreWebUI.Models.Products
{
    public class ProductViewModel
    {
        public List<Product> ProductList { get; set; }
        public Product Product { get; set; }
    }
}