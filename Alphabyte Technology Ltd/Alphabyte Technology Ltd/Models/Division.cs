using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Alphabyte_Technology_Ltd_.Models
{
    public class Division
    {
        [Key]
        [Required]
        public int DivisionId { get; set; }

        [Required]
        [MaxLength(20)]
        public string? DivisionName { get; set; }  
    }
}
