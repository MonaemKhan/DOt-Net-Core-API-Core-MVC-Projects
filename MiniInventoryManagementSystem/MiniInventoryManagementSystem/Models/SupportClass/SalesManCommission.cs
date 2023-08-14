using System.ComponentModel.DataAnnotations;

namespace MiniInventoryManagementSystem.Models.SupportClass
{
    public class SalesManCommission
    {
        public string SalesManName { get; set; }
        public string SalesManDesignation { get; set; }
        public double CurrentMonthTotalSales { get; set; }
        public double CurrentdateTotalSales { get; set; }
        public double CureentMonthCommission { set; get; }
        public double CurrentDateCommission { set; get; }
    }
}
