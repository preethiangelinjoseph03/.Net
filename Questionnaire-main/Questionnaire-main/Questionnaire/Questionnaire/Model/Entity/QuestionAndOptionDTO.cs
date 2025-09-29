using Questionnaire.Model.DbSet;

namespace Questionnaire.Model.Entity
{
    public class QuestionAndOptionDTO
    {
        public int LanguageId { get; set; }
        public int QuestionId { get; set; }
        public QuestionCategory Category { get; set; }
        public string Question { get; set; }
        public List<string> Options { get; set; }
    }
}