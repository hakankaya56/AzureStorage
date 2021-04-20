using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Cosmos.Table;

namespace AzureProject.Entities.Entities.Azure
{
   public class Product:TableEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Brand { get; set; }
        public int Stock { get; set; }
    }
}
