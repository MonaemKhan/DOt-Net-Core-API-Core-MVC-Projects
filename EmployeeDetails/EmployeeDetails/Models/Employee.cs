using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace EmployeeDetails.Models
{
    public class Employee
    {
        [Key]
        [Required]
        [DisplayName("Employee Id")]
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Name")]
        public string? Name { get; set; }

        [Required]
        [MaxLength(10)]
        [DisplayName("Gender")]
        public string? Gender { get; set; }

        [Required]
        [DisplayName("SSC")]
        public bool SSC { get; set; }

        [Required]
        [DisplayName("HSC")]
        public bool HSC { get; set; }

        [Required]
        [DisplayName("BSC")]
        public bool BSC { get; set; }

        [Required]
        [DisplayName("MSC")]
        public bool MSC { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Address")]
        public string? Address { get; set; }

        [Required]
        [DisplayName("Country Id")]
        [ForeignKey("Country")]
        public int countryId { get; set; }
        public Country? Employee_country { get; set; }

        [Required]
        [DisplayName("State Id")]
        [ForeignKey("State")]
        public int stateId { get; set; }
        public State? Employee_State { get; set; }

        [Required]
        [DisplayName("City Id")]
        [ForeignKey("City")]
        public int cityId { get; set; }
        public City? Employee_City { get; set; }

        [DisplayName("Image Name")]
        public string? ImageName { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
