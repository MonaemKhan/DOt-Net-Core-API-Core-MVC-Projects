using System.ComponentModel.DataAnnotations;

namespace MiniInventoryManagementSystem.Models.SupportClass
{
    public class StockAndProfitOverview
    {
        public string CatagoryName { get; set; }
        public string ProductName { get; set; }
        public int PurchesDetailsQuantity { get; set; }
        public double PurchesDetailsPrice { get; set; }
        public double ParchesDetailsTotalPrice { get; set; }
        public int SalesDetailsQuantity { get; set; }
        public double SalesDetailsPrice { get; set; }
        public double SalesDetailsTotalPrice { get; set; }
        public double Profit { get; set; }
        public int stock { get; set; }
    }
}
