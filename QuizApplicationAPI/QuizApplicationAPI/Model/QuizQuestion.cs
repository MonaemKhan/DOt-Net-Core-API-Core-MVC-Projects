using System.ComponentModel.DataAnnotations;

namespace QuizApplicationAPI.Model
{
    public class QuizQuestion
    {
        [Key]
        public int QuizId { get; set; }
        public string? Question { get; set; }
        public string? Option1 { get; set; }
        public string? Option2 { get; set; }
        public string? Option3 { get; set; }
        public string? Option4 { get; set; }
        public string? Right_Answer { get; set; }
        public int Mark { get; set; }
    }
}
