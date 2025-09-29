namespace Questionnaire.Model.DbSet
{
    public class AnswerEvaluation
    {
        public int Id { get; set; }
        public int CandidateAnswerId { get; set; } 
        public bool Evaluation {  get; set; }
        public string? Remarks { get; set; }

        public CandidateAnswers CandidateAnswer { get; set; }
    }
}