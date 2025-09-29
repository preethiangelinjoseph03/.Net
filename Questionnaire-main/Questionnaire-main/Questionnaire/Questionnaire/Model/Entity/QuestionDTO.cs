using System.ComponentModel.DataAnnotations;
using Questionnaire.Model.DbSet;

namespace Questionnaire.Model.Entity
{
    public class QuestionDTO 
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        //public string? UserId { get; set; }
        [Required]
        public string QuestionText { get; set; } = null!;
        public int Category { get; set; }
        public string? AnswerText { get; set; }

    }
}