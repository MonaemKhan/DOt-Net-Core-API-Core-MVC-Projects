using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmplyeeWebAPI.Model
{
    public class EmployeeDetails
    {
        [Key]
        [Required]
        public int employeeId { get; set; }

        [Required]
        [MaxLength(30)]
        public string? employeeName { get; set; }

        [Required]
        [MaxLength(10)]
        public string? employeeCode { get; set; }

        [Required]
        public long employeeSalary { get; set; }

        [Required]
        public int supervisorId { get; set; }
    }
}
