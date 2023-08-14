using System.ComponentModel.DataAnnotations;

namespace MiniInventoryManagementSystem.Models.SupportClass
{
    public class SalesManCommission
    {
        [Key]
        public int id {  get; set; }
        public string  ProductName { get; set; }
        public string SalesManName { get; set; }
        public string SalesManDesignation { get; set; }
        public double TotalSalesDetailsPrice { get; set; }
        public int TotalSalesDetailsQuantity { get; set; }
        public double TotalPurchesDetailsPrice { get; set; }

        public double Commission { set; get; }
    }
}
