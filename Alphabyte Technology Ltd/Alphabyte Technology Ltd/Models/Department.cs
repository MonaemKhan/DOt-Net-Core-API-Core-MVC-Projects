using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Alphabyte_Technology_Ltd_.Models
{
    public class Department
    {
        [Key]
        [Required]
        public int Dept_Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string? DeptName { get; set; }

        [Required]
        [ForeignKey("DivisionId")]
        [DisplayName("Division Id")]
        public int Department_DivisionId { get; set; }
        public Division? Department_Division { get; set; }
    }
}
