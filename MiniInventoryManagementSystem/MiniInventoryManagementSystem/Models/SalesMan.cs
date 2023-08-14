using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MiniInventoryManagementSystem.Models
{
    public class SalesMan
    {
        [Key]
        [Required]
        [DisplayName("Id")]
        public int SalesManId { get; set; }

        [Required]
        [DisplayName("Name")]
        [MaxLength(20)]
        public string SalesManName { get; set; }

        [Required]
        [DisplayName("Date Of Birth")]
        [MaxLength(20)]
        public string SalesManDOB { get; set; }

        [Required]
        [DisplayName("Address")]
        [MaxLength(50)]
        public string SalesManAddress { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        [MaxLength(20)]
        public string SalesManPhone { get; set; }

        [Required]
        [DisplayName("Email")]
        [MaxLength(30)]
        public string SalesManEmail { get; set; }

        [Required]
        [DisplayName("Designation")]
        [MaxLength(20)]
        public string SalesManDesignation { get; set; }

        [Required]
        [DisplayName("Salary")]
        [MaxLength(20)]
        public string SalesManSalary { get; set; }

        [Required]
        [DisplayName("Joining Date")]
        [MaxLength(20)]
        public string SalesManJoiningDate { get; set; }       
    }
}