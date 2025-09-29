using Microsoft.EntityFrameworkCore;
using Questionnaire.Data;
using Questionnaire.Interface;
using Questionnaire.Model.DbSet;
using Questionnaire.Model.Entity;

namespace Questionnaire.Repository
{
    public class QuestionnaireRepository(QuestionnaireDBContext context) : IQuestionnaireRepository
    {
        public async Task<List<QuestionAndOptionDTO>> GetQuestionsAndOptionsAsync(int count, List<int> languageIds)
        {
            // Step 1: Fetch all questions filtered by selected languages
            var allMatchingQuestions = await context.Question
                .Where(q => languageIds.Contains(q.LanguageId))
                .ToListAsync();

            // Step 2: Randomly select 'count' questions from the fetched list
            var random = new Random();
            var selectedQuestions = allMatchingQuestions
                .OrderBy(q => random.Next())
                .Take(count)
                .ToList();

            // Step 3: Prepare result DTO list
            var questionAndOptions = new List<QuestionAndOptionDTO>();

            foreach (var question in selectedQuestions)
            {
                List<string> options = new();

                // For Option and Checkbox questions, fetch options
                if (question.Category != QuestionCategory.Freetext)
                {
                    options = await context.OptionsAndAnswer
                        .Where(o => o.QuestionId == question.Id && !string.IsNullOrEmpty(o.OptionText))
                        .Select(o => o.OptionText)
                        .ToListAsync();
                }

                questionAndOptions.Add(new QuestionAndOptionDTO
                {
                    QuestionId = question.Id,
                    Question = question.QuestionText,
                    LanguageId = question.LanguageId,
                    Category = question.Category,
                    Options = options
                });
            }

            return questionAndOptions;
        }
    }
}
