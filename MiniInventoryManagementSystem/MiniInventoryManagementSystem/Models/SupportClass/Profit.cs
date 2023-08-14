using System.ComponentModel.DataAnnotations;

namespace MiniInventoryManagementSystem.Models.SupportClass
{
    public class Profit
    {
        public string CatagoryName { get; set; }
        public string ProductName { get; set; }
        public int TotalSaleDetailsQuentity { get; set; }
        public double ParchesDetailsTotalPrice { get; set; }
        public double SalesDetailsTotalPrice { get; set; }
        public double Net_Profit { get; set; }
    }
}
