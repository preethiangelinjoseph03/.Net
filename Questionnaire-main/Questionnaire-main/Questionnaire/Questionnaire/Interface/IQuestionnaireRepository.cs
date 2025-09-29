using Questionnaire.Model.Entity;

namespace Questionnaire.Interface
{
    public interface IQuestionnaireRepository
    {
        Task<List<QuestionAndOptionDTO>> GetQuestionsAndOptionsAsync(int count, List<int> languageIds);
    }
}
