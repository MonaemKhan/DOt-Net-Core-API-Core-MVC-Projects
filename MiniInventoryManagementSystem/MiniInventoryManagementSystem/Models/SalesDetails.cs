using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MiniInventoryManagementSystem.Models
{
    public class SalesDetails
    {
        [Key]
        [Required]
        [DisplayName("Sales Details Id")]
        public int SalesDetailsId { get; set; }

        [Required]
        [DisplayName("Sales Price")]
        public double SalesDetailsPrice { get; set; }

        [Required]
        [DisplayName("Sales Quantity")]
        public int SalesDetailsQuantity { get; set; }

        [Required]
        [DisplayName("Sales Id")]
        [ForeignKey("SalesId")]
        [Column("SalesId")]
        public int SalesDetails_SalesId { get; set; }
        public Sales SalesDetails_Sales { get; set; }

        [Required]
        [DisplayName("Product Id")]
        [ForeignKey("ProductId")]
        public int SalesDetails_ProductId { get; set; }
        public Product SalesDetails_Product { get; set; }
    }
}
