using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniInventoryManagementSystem.Models
{
    public class PurchesDetails
    {
        [Key]
        [Required]
        [DisplayName("Purches Details Id")]
        public int PurchesDetailsId { get; set; }

        [Required]
        [DisplayName("Purches Id")]
        [ForeignKey("PurchesId")]
        [Column("PurchesId")]
        public int PurchesDetails_PurchesId { get; set; }
        public Purches PurchesDetails_Purches { get; set; }

        [Required]
        [DisplayName("Product Id")]
        [ForeignKey("ProductId")]
        [Column("ProductId")]
        public int PurchesDetails_ProductId { get; set; }
        public Product PurchesDetails_Product { get; set; }

        [Required]
        [DisplayName("Purches Details Price")]
        public double PurchesDetailsPrice { get; set; }

        [Required]
        [DisplayName("Purches Details Quantity")]
        public double PurchesDetailsQuantity { get; set; }
    }
}
