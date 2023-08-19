using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EmployeeDetails.Models
{
    public class Country
    {

        [Key]
        [Required]
        [DisplayName("Country Id")]
        public int CountryId { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Country Name")]
        public string CountryName { get; set; }
    }
}
