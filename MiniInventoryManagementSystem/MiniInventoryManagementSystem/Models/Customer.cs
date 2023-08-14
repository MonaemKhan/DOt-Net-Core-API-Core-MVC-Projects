using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiniInventoryManagementSystem.Models
{
    public class Customer
    {
        [Key]
        [Required]
        [DisplayName("Customer Id")]
        public int CustomerId { get; set; }

        [Required]
        [DisplayName("Customer Name")]
        [MaxLength(20)]
        public string CustomerName { get; set; }

        [Required]
        [DisplayName("Customer Address")]
        [MaxLength(50)]
        public string CustomerAddress { get; set; }

        [Required]
        [DisplayName("Customer Phone Number")]
        [MaxLength(20)]
        public string CustomerPhone { get; set; }

        [Required]
        [DisplayName("Customer Email")]
        [MaxLength(30)]
        public string CustomerEmail { get; set; }
    }
}
