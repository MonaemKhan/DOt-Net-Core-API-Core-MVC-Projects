using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MiniInventoryManagementSystem.Models
{
    public class Sales
    {
        [Key]
        [Required]
        [DisplayName("Sales Id")]
        public int SalesId { get; set; }

        [Required]
        [DisplayName("SalesMan's Id")]
        [ForeignKey("SalesManID")]
        [Column("SalesManID")]
        public int Sales_SalesManId { get; set; }
        public SalesMan Sales_SalesMan { get; set; }

        [Required]
        [DisplayName("Customer's Id")]
        [ForeignKey("CustomerId")]
        [Column("CustomerId")]
        public int Sales_CustomerId { get; set; }
        public Customer Sales_Customer { get; set; }

        [Required]
        [DisplayName("Sales Date")]
        [MaxLength(20)]
        public string SalesDate { get; set; }
    }
}
