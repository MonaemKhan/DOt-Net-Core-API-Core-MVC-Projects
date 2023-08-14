using System.ComponentModel.DataAnnotations;

namespace MiniInventoryManagementSystem.Models.SupportClass
{
    public class Stock
    {
        public string CatagoryName { get; set; }
        public string ProductName { get; set; }
        public int PurchesDetailsQuantity { get; set; }
        public int SalesDetailsQuantity { get; set; }
        public int stock { get; set; }
    }
}
