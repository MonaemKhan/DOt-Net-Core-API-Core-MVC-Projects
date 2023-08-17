using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiniInventoryManagementSystem.Models.SupportClass
{
    public class SalesManCommission
    {
        [DisplayName("SalesMan")]
        public string SalesManName { get; set; }

        [DisplayName("Designation")]
        public string SalesManDesignation { get; set; }

        [DisplayName("Sale")]
        public double CurrentMonthTotalSales { get; set; }

        [DisplayName("Sale")]
        public double CurrentdateTotalSales { get; set; }

        [DisplayName("Commisstion")]
        public double CureentMonthCommission { set; get; }

        [DisplayName("Commisstion")]
        public double CurrentDateCommission { set; get; }
    }
}
