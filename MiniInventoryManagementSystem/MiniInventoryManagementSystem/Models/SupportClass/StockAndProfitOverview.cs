using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiniInventoryManagementSystem.Models.SupportClass
{
    public class StockAndProfitOverview
    {
        [DisplayName("Catagory")]
        public string CatagoryName { get; set; }

        [DisplayName("Product")]
        public string ProductName { get; set; }

        [DisplayName("Purches Quentity")]
        public int PurchesDetailsQuantity { get; set; }

        [DisplayName("Purches Price")]
        public double PurchesDetailsPrice { get; set; }

        [DisplayName("Total Purches(TK)")]
        public double ParchesDetailsTotalPrice { get; set; }

        [DisplayName("Sale Quentity")]
        public int SalesDetailsQuantity { get; set; }

        [DisplayName("Sale Price")]
        public double SalesDetailsPrice { get; set; }

        [DisplayName("Total Sale(TK)")]
        public double SalesDetailsTotalPrice { get; set; }

        [DisplayName("Profit/Loss")]
        public double Profit { get; set; }

        [DisplayName("Available Product")]
        public int stock { get; set; }
    }
}
