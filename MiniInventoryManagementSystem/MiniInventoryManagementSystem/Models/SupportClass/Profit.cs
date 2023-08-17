using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiniInventoryManagementSystem.Models.SupportClass
{
    public class Profit
    {
        [DisplayName("Catagory")]
        public string CatagoryName { get; set; }

        [DisplayName("Product")]
        public string ProductName { get; set; }

        [DisplayName("Sale Quentity")]
        public int TotalSaleDetailsQuentity { get; set; }

        [DisplayName("Purches Price")]
        public double ParchesDetailsTotalPrice { get; set; }

        [DisplayName("SalePrice")]
        public double SalesDetailsTotalPrice { get; set; }

        [DisplayName("Profit/Loss")]
        public double Net_Profit { get; set; }
    }
}
