using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Alphabyte_Technology_Ltd_.Models
{
    public class InterViewerDetails
    {
        [Key]
        [StringLength(8,MinimumLength =8)]
        public string? Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        [ForeignKey("DivisionId")]
        public int InterViewerDetails_DivisionId { get; set; }
        public Division? InterViewerDetails_Division { get; set; }

        [Required]
        [ForeignKey("Dept_Id")]
        public int InterViewerDetails_DepartmentDept_Id { get; set; }
        public Department? InterViewerDetails_Department { get; set; }

        [Required,MaxLength(50)]
        public string? DoB { get; set;}

        [Required,MaxLength(1000)]
        public string? ResumeFile { get; set; }

        [Required]
        [NotMapped]
        public IFormFile? UploadResume { get; set; }
    }
}
