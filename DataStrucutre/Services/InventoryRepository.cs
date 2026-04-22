using DataStrucutre.DataStructure;
using DataStrucutre.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DataStrucutre.Services
{
    public class InventoryRepository
    {
        private readonly XmlSerializer serializer = new XmlSerializer(typeof(InventoryStore));

        public InventoryRepository()
            : this(GetDefaultFilePath())
        {
        }

        public InventoryRepository(string filePath)
        {
            FilePath = filePath;
        }

        public string FilePath { get; private set; }

        public InventoryStore Load()
        {
            if (!File.Exists(FilePath))
            {
                InventoryStore seededStore = CreateSeedStore();
                Save(seededStore);
                return seededStore;
            }

            using (FileStream stream = File.OpenRead(FilePath))
            {
                InventoryStore store = serializer.Deserialize(stream) as InventoryStore;
                return EnsureStore(store);
            }
        }

        public void Save(InventoryStore store)
        {
            InventoryStore safeStore = EnsureStore(store);
            safeStore.LastSavedAt = DateTime.Now;

            string directory = Path.GetDirectoryName(FilePath);
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (FileStream stream = File.Create(FilePath))
            {
                serializer.Serialize(stream, safeStore);
            }
        }

        public void ExportProductsCsv(string exportPath, IEnumerable<Product> products)
        {
            List<string> lines = new List<string>
            {
                "Product Id,SKU,Name,Category,Supplier,Original Price,Offer %,Offer Price,Tax %,Final Price,Stock,Low Stock,Status,Inventory Value,Updated At"
            };

            foreach (Product product in products)
            {
                lines.Add(string.Join(",",
                    Escape(product.ProductId),
                    Escape(product.Sku),
                    Escape(product.ProductName),
                    Escape(product.ProductCategory.ToString()),
                    Escape(product.Supplier),
                    product.OriginalPrice.ToString("0.00", CultureInfo.InvariantCulture),
                    product.OfferPercentage.ToString("0.##", CultureInfo.InvariantCulture),
                    product.OfferPrice.ToString("0.00", CultureInfo.InvariantCulture),
                    product.Tax.ToString("0.##", CultureInfo.InvariantCulture),
                    product.FinalPrice.ToString("0.00", CultureInfo.InvariantCulture),
                    product.Stock.ToString(CultureInfo.InvariantCulture),
                    product.LowStock.ToString(CultureInfo.InvariantCulture),
                    Escape(product.StockInformation.ToString()),
                    product.InventoryValue.ToString("0.00", CultureInfo.InvariantCulture),
                    Escape(product.UpdatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture))));
            }

            File.WriteAllLines(exportPath, lines, Encoding.UTF8);
        }

        public static InventoryStore CreateSeedStore()
        {
            InventoryStore store = new InventoryStore();

            store.Users.Add(UserAccount.Create("EMP-001", "System Administrator", "admin@stockflow.local", "admin123", UserLevel.SuperAdmin));

            store.Products.Add(new Product
            {
                Sku = "EL-KEY-001",
                ProductName = "Wireless Keyboard",
                ProductCategory = ProductCategory.Electronics,
                Supplier = "Northwind Traders",
                OriginalPrice = 1299m,
                OfferPercentage = 10m,
                Tax = 18m,
                Stock = 42,
                LowStock = 8,
                CreatedAt = DateTime.Now.AddDays(-18),
                UpdatedAt = DateTime.Now.AddDays(-2)
            });

            store.Products.Add(new Product
            {
                Sku = "OF-NBK-120",
                ProductName = "A5 Notebook Pack",
                ProductCategory = ProductCategory.OfficeSupplies,
                Supplier = "PaperTrail Supply",
                OriginalPrice = 240m,
                OfferPercentage = 0m,
                Tax = 5m,
                Stock = 120,
                LowStock = 25,
                CreatedAt = DateTime.Now.AddDays(-14),
                UpdatedAt = DateTime.Now.AddDays(-1)
            });

            store.Products.Add(new Product
            {
                Sku = "GR-RCE-050",
                ProductName = "Premium Rice 5kg",
                ProductCategory = ProductCategory.Grocery,
                Supplier = "Harvest House",
                OriginalPrice = 620m,
                OfferPercentage = 4m,
                Tax = 5m,
                Stock = 6,
                LowStock = 10,
                CreatedAt = DateTime.Now.AddDays(-10),
                UpdatedAt = DateTime.Now
            });

            store.Products.Add(new Product
            {
                Sku = "HW-DRV-042",
                ProductName = "Screwdriver Set",
                ProductCategory = ProductCategory.Hardware,
                Supplier = "BuildRight Tools",
                OriginalPrice = 799m,
                OfferPercentage = 12m,
                Tax = 18m,
                Stock = 0,
                LowStock = 5,
                CreatedAt = DateTime.Now.AddDays(-9),
                UpdatedAt = DateTime.Now.AddHours(-5)
            });

            return store;
        }

        private static InventoryStore EnsureStore(InventoryStore store)
        {
            InventoryStore safeStore = store ?? new InventoryStore();
            safeStore.Products = safeStore.Products ?? new List<Product>();
            safeStore.Users = safeStore.Users ?? new List<UserAccount>();

            if (!safeStore.Users.Any())
            {
                safeStore.Users.Add(UserAccount.Create("EMP-001", "System Administrator", "admin@stockflow.local", "admin123", UserLevel.SuperAdmin));
            }

            return safeStore;
        }

        private static string GetDefaultFilePath()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(appData, "StockFlow", "inventory-data.xml");
        }

        private static string Escape(string value)
        {
            string safeValue = value ?? string.Empty;

            if (safeValue.Contains(",") || safeValue.Contains("\"") || safeValue.Contains(Environment.NewLine))
            {
                return "\"" + safeValue.Replace("\"", "\"\"") + "\"";
            }

            return safeValue;
        }
    }
}
