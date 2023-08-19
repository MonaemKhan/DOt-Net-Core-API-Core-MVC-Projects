using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeDetails.Models
{
    public class State
    {

        [Key]
        [Required]
        [DisplayName("State Id")]
        public int StateId { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("State Name")]
        public string StateName { get; set; }

        [Required]
        [ForeignKey("CountryId")]
        [DisplayName("Country Id")]
        public int State_CountryId { get; set; }

        public Country State_country { get; set; }

    }
}
