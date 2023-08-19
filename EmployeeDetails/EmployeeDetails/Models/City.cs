using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeDetails.Models
{
    public class City
    {
        [Key]
        [Required]
        public int CityId { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("City Name")]
        public string CityName { get; set; }

        [ForeignKey("StateId")]
        [Required]
        [DisplayName("State Id")]
        public int City_StateId { get; set; }
        public State City_state { get; set; }
    }
}
