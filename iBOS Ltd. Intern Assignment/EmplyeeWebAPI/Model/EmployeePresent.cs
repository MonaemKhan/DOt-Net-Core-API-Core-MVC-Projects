using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmplyeeWebAPI.Model
{
    public class EmployeePresent
    {
        [Key]
        public int Id {  get; set; }

        [Required]
        public int employeeId { get; set; }

        [Required]
        [MaxLength(30)]
        public string? attendenceDate { get; set; }

        [Required]
        public int isPresent { get; set; }

        [Required]
        public int isAbsent { get; set; }

        [Required]
        public int isOffday { get; set; }

    }
}
