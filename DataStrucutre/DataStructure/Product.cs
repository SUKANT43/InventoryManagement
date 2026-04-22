using DataStrucutre.Enums;
using System;
using System.Globalization;
using System.Xml.Serialization;

namespace DataStrucutre.DataStructure
{
    public class Product
    {
        public Product()
        {
            ProductId = CreateProductId();
            ProductName = string.Empty;
            Sku = string.Empty;
            Supplier = string.Empty;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public Product(
            string productId,
            string productName,
            ProductCategory productCategory,
            decimal originalPrice,
            decimal offerPercentage,
            decimal tax,
            int stock,
            int lowStock)
            : this()
        {
            ProductId = string.IsNullOrWhiteSpace(productId) ? CreateProductId() : productId;
            ProductName = productName;
            ProductCategory = productCategory;
            OriginalPrice = originalPrice;
            OfferPercentage = offerPercentage;
            Tax = tax;
            Stock = stock;
            LowStock = lowStock;
        }

        public string ProductId { get; set; }
        public string Sku { get; set; }
        public string ProductName { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public string Supplier { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal OfferPercentage { get; set; }
        public decimal Tax { get; set; }
        public int Stock { get; set; }
        public int LowStock { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [XmlIgnore]
        public decimal OfferPrice
        {
            get
            {
                decimal discount = OriginalPrice * OfferPercentage / 100m;
                return Math.Round(OriginalPrice - discount, 2);
            }
        }

        [XmlIgnore]
        public decimal TaxAmount
        {
            get { return Math.Round(OfferPrice * Tax / 100m, 2); }
        }

        [XmlIgnore]
        public decimal FinalPrice
        {
            get { return Math.Round(OfferPrice + TaxAmount, 2); }
        }

        [XmlIgnore]
        public decimal InventoryValue
        {
            get { return Math.Round(FinalPrice * Stock, 2); }
        }

        [XmlIgnore]
        public StockInformation StockInformation
        {
            get
            {
                if (Stock <= 0)
                {
                    return StockInformation.OutOfStock;
                }

                return Stock <= LowStock ? StockInformation.LowStock : StockInformation.InStock;
            }
        }

        public Product Clone()
        {
            return new Product
            {
                ProductId = ProductId,
                Sku = Sku,
                ProductName = ProductName,
                ProductCategory = ProductCategory,
                Supplier = Supplier,
                OriginalPrice = OriginalPrice,
                OfferPercentage = OfferPercentage,
                Tax = Tax,
                Stock = Stock,
                LowStock = LowStock,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt
            };
        }

        public static string CreateProductId()
        {
            return "PRD-" + DateTime.Now.ToString("yyMMdd", CultureInfo.InvariantCulture) + "-" +
                   Guid.NewGuid().ToString("N").Substring(0, 6).ToUpperInvariant();
        }
    }
}
