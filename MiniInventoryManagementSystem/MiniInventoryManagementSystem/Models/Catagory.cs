using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiniInventoryManagementSystem.Models
{
    public class Catagory
    {
        [Key]
        [Required]
        [DisplayName("Catagory Id")]
        public int CatagoryId { get; set; }

        [Required]
        [DisplayName("Catagory Name")]
        [MaxLength(20)]
        public string CatagoryName { get; set; }
    }
}
