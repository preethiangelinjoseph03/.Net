using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Questionnaire.Model.DbSet
{
    public class Question
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }             
        [Required]
        public string QuestionText { get; set; } = null!;        
        [Required]
        public QuestionCategory Category { get; set; }
        public string? Answertext { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Language? Language { get; set; }

        //[ForeignKey("UserId")]
        //public ApplicationUser User { get; set; }
    }

    public enum QuestionCategory
    {
        Freetext,
        Option,
        Checkbox
    }
}