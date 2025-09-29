using System.Text.Json.Serialization;
using Questionnaire.Model.DbSet;

namespace Questionnaire.Model.Entity
{
    public class CandidateAnswerDTO
    {
        public int Id { get; set; }                
        public string? CandidateId { get; set; }
        public int LanguageId { get; set; }
        public int QuestionId { get; set; }
        public string? Answer { get; set; }
        
        public DateTime? CompletedAt { get; set; }
        public int Order { get; set; }
    }
}