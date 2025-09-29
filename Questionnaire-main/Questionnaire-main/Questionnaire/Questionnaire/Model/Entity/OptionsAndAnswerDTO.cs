using System.Text.Json.Serialization;
using Questionnaire.Model.DbSet;

namespace Questionnaire.Model.Entity
{
    public class OptionsAndAnswerDTO
    {        
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string? OptionText { get; set; }
        public bool IsCorrect { get; set; }  
    }
}