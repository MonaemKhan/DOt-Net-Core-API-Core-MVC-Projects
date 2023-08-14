using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MiniInventoryManagementSystem.Models
{
    public class Supplyer
    {
        [Key]
        [Required]
        [DisplayName("Supplyer Id")]
        public int SupplyerId { get; set; }

        [Required]
        [DisplayName("Supplyer Name")]
        [MaxLength(20)]
        public string SupplyerName { get; set; }

        [Required]
        [DisplayName("Supplyer Address")]
        [MaxLength(50)]
        public string SupplyerAddress { get; set; }

        [Required]
        [DisplayName("Supplyer Phone Number")]
        [MaxLength(20)]
        public string SupplyerPhone { get; set; }
    }
}
