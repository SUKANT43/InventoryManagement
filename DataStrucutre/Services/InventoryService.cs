using DataStrucutre.DataStructure;
using DataStrucutre.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStrucutre.Services
{
    public class InventoryService
    {
        private readonly InventoryRepository repository;
        private InventoryStore store;

        public InventoryService()
            : this(new InventoryRepository())
        {
        }

        public InventoryService(InventoryRepository repository)
        {
            this.repository = repository;
            store = repository.Load();
        }

        public string DataFilePath
        {
            get { return repository.FilePath; }
        }

        public IReadOnlyList<Product> Products
        {
            get
            {
                return store.Products
                    .OrderBy(product => product.ProductName)
                    .ThenBy(product => product.Sku)
                    .ToList();
            }
        }

        public UserAccount Authenticate(string email, string password)
        {
            string normalizedEmail = Normalize(email);

            return store.Users.FirstOrDefault(user =>
                user.IsActive &&
                Normalize(user.Email) == normalizedEmail &&
                user.VerifyPassword(password));
        }

        public IReadOnlyList<Product> SearchProducts(string searchText, ProductCategory? category)
        {
            IEnumerable<Product> query = store.Products;

            if (category.HasValue)
            {
                query = query.Where(product => product.ProductCategory == category.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                string search = searchText.Trim();
                query = query.Where(product =>
                    Contains(product.ProductName, search) ||
                    Contains(product.Sku, search) ||
                    Contains(product.Supplier, search) ||
                    Contains(product.ProductId, search));
            }

            return query
                .OrderBy(product => product.ProductName)
                .ThenBy(product => product.Sku)
                .ToList();
        }

        public Product GetProduct(string productId)
        {
            Product product = store.Products.FirstOrDefault(item => item.ProductId == productId);
            return product == null ? null : product.Clone();
        }

        public Product SaveProduct(Product product)
        {
            ValidateProduct(product);

            Product existing = store.Products.FirstOrDefault(item => item.ProductId == product.ProductId);

            if (existing == null)
            {
                product.ProductId = string.IsNullOrWhiteSpace(product.ProductId)
                    ? Product.CreateProductId()
                    : product.ProductId;
                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;
                store.Products.Add(product.Clone());
                Save();
                return product.Clone();
            }

            existing.Sku = Clean(product.Sku);
            existing.ProductName = Clean(product.ProductName);
            existing.ProductCategory = product.ProductCategory;
            existing.Supplier = Clean(product.Supplier);
            existing.OriginalPrice = product.OriginalPrice;
            existing.OfferPercentage = product.OfferPercentage;
            existing.Tax = product.Tax;
            existing.Stock = product.Stock;
            existing.LowStock = product.LowStock;
            existing.UpdatedAt = DateTime.Now;

            Save();
            return existing.Clone();
        }

        public void DeleteProduct(string productId)
        {
            Product existing = store.Products.FirstOrDefault(item => item.ProductId == productId);
            if (existing == null)
            {
                return;
            }

            store.Products.Remove(existing);
            Save();
        }

        public Product AdjustStock(string productId, int quantityChange)
        {
            Product existing = store.Products.FirstOrDefault(item => item.ProductId == productId);
            if (existing == null)
            {
                throw new InvalidOperationException("Select a product before adjusting stock.");
            }

            int updatedStock = existing.Stock + quantityChange;
            if (updatedStock < 0)
            {
                throw new InvalidOperationException("Stock cannot be less than zero.");
            }

            existing.Stock = updatedStock;
            existing.UpdatedAt = DateTime.Now;
            Save();
            return existing.Clone();
        }

        public void ExportProductsCsv(string exportPath, IEnumerable<Product> products)
        {
            repository.ExportProductsCsv(exportPath, products);
        }

        public void ResetDemoData()
        {
            store = InventoryRepository.CreateSeedStore();
            Save();
        }

        private void ValidateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("product");
            }

            product.Sku = Clean(product.Sku);
            product.ProductName = Clean(product.ProductName);
            product.Supplier = Clean(product.Supplier);

            if (string.IsNullOrWhiteSpace(product.ProductName))
            {
                throw new ArgumentException("Product name is required.");
            }

            if (product.OriginalPrice < 0)
            {
                throw new ArgumentException("Original price cannot be negative.");
            }

            if (product.OfferPercentage < 0 || product.OfferPercentage > 100)
            {
                throw new ArgumentException("Offer percentage must be between 0 and 100.");
            }

            if (product.Tax < 0 || product.Tax > 100)
            {
                throw new ArgumentException("Tax percentage must be between 0 and 100.");
            }

            if (product.Stock < 0)
            {
                throw new ArgumentException("Stock cannot be negative.");
            }

            if (product.LowStock < 0)
            {
                throw new ArgumentException("Low stock threshold cannot be negative.");
            }

            if (!string.IsNullOrWhiteSpace(product.Sku))
            {
                bool duplicateSku = store.Products.Any(existing =>
                    !string.Equals(existing.ProductId, product.ProductId, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(existing.Sku, product.Sku, StringComparison.OrdinalIgnoreCase));

                if (duplicateSku)
                {
                    throw new ArgumentException("Another product already uses this SKU.");
                }
            }
        }

        private void Save()
        {
            repository.Save(store);
        }

        private static bool Contains(string source, string search)
        {
            return (source ?? string.Empty).IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static string Normalize(string value)
        {
            return (value ?? string.Empty).Trim().ToLowerInvariant();
        }

        private static string Clean(string value)
        {
            return (value ?? string.Empty).Trim();
        }
    }
}
