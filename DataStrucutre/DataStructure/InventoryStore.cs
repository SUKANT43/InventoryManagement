using System;
using System.Collections.Generic;

namespace DataStrucutre.DataStructure
{
    public class InventoryStore
    {
        public InventoryStore()
        {
            Products = new List<Product>();
            Users = new List<UserAccount>();
            LastSavedAt = DateTime.Now;
        }

        public List<Product> Products { get; set; }
        public List<UserAccount> Users { get; set; }
        public DateTime LastSavedAt { get; set; }
    }
}
