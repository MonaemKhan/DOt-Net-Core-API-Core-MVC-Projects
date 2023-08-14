using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MiniInventoryManagementSystem.Models.SupportClass
{
    public class Purches_And_PurchesDetails
    {
        //perches details
        [Key]
        [Required]
        [DisplayName("Purches Details Id")]
        public int PurchesDetailsId { get; set; }

        [Required]
        [DisplayName("Product Id")]
        public int PurchesDetails_ProductId { get; set; }
        public string ProductName { get; set; }
        public string catagoryName { get; set; }

        [Required]
        [DisplayName("Purches Details Price")]
        public double PurchesDetailsPrice { get; set; }

        [Required]
        [DisplayName("Purches Details Quantity")]
        public double PurchesDetailsQuantity { get; set; }

        //purches
        [Required]
        [DisplayName("Purches Id")]
        public int PurchesId { get; set; }

        [Required]
        [DisplayName("Purches Date")]
        [MaxLength(30)]
        public string PurchesDate { get; set; }

        [Required]
        [DisplayName("Supplyer Name")]
        public int Purches_SupplyerId { get; set; }
        public string SupplyerName { get; set; }

        public double TotalPrice { get; set; }
    }
}
