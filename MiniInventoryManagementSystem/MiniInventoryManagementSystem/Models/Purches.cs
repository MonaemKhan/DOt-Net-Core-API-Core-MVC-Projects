using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniInventoryManagementSystem.Models
{
    public class Purches
    {
        [Key]
        [Required]
        [DisplayName("Purches Id")]
        public int PurchesId { get; set; }

        [Required]
        [DisplayName("Purches Date")]
        [MaxLength(30)]
        public string PurchesDate { get; set; }

        [Required]
        [DisplayName("Supplyer Name")]
        [ForeignKey("SupplyerId")]
        [Column("SupplyerId")]
        public int Purches_SupplyerId { get; set; }
        public Supplyer Purches_Supplyer { get; set; }
    }
}
