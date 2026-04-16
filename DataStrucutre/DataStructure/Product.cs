using DataStrucutre.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStrucutre.DataStructure
{
    public class Product
    {
        public string ProductId { get; }
        public string ProductName { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal OfferPercentage { get; set; }
        public decimal OfferPrice { get; }
        public decimal Tax { get; set; }
        public int Stock { get; set; }
        public int LowStock { get; set; }

        public Product(string productId, string productName, ProductCategory productCategory, decimal originalPrice, decimal offerPercentage, decimal tax, int stock, int lowStock)
        {
            ProductId = productId;
            ProductName = productName;
            ProductCategory = productCategory;
            OriginalPrice = originalPrice;
            OfferPercentage = offerPercentage;
            Tax = tax;
            Stock = stock;
            LowStock = lowStock;

            OfferPrice = originalPrice - (originalPrice * offerPercentage / 100);
        }
    }
}
