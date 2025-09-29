using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Questionnaire.Model.DbSet
{
    public class CandidateAnswers
    {
        public int Id { get; set; }     
        public int LanguageId { get; set; }
        public int? QuestionId { get; set; }
        public string? CandidateId { get; set; }
        public string? CandidateAnswer { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int Order { get; set; }
        
        public Language Language { get; set; }
        public Question Question { get; set; }

        [ForeignKey("CandidateId")]
        public ApplicationUser Candidate { get; set; }
    }
}
