namespace Questionnaire.Model.DbSet
{
    public class OptionsAndAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string? OptionText { get; set; } 
        public bool IsCorrect {  get; set; }        
        public Question? Question { get; set; }
    }
}