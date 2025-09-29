namespace Questionnaire.Model.Entity
{
    public class AnswerEvaluationDTO
    {
        public int Id { get; set; }
        public int CandidateanswerId { get; set; }
        public bool Evaluation { get; set; }
        public string? Remarks { get; set; }
    }
}