using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiniInventoryManagementSystem.Models.SupportClass
{
    public class Stock
    {
        [DisplayName("Catagory")]
        public string CatagoryName { get; set; }

        [DisplayName("Product")]
        public string ProductName { get; set; }

        [DisplayName("Purches Quentity")]
        public int PurchesDetailsQuantity { get; set; }

        [DisplayName("Sale Quentity")]
        public int SalesDetailsQuantity { get; set; }

        [DisplayName("Available Product")]
        public int stock { get; set; }
    }
}
