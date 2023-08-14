using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniInventoryManagementSystem.Models
{
    public class Product
    {
        [Key]
        [Required]
        [DisplayName("Product Id")]
        public int ProductId { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [Required]
        [DisplayName("Catagory Name")]
        [ForeignKey("CatagoryId")]
        [Column("CatagoryId")]
        public int Product_CatagoryId { get; set; }

        [DisplayName("Product Catagory")]
        public Catagory Product_Catagory { get; set; }

    }
}
