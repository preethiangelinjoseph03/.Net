using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Questionnaire.Interface;
using Questionnaire.Model.DbSet;
using Questionnaire.Model.Entity;
using Questionnaire.Repository.Interface;

namespace Questionnaire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class QuestionAndOptionController(        
        IQuestionnaireRepository questionnairerepo,
        ILogger<QuestionAndOptionController> logger) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin,HR,Candidate")]
        public async Task<IActionResult> GetQuestionsAndOptions(int count, [FromQuery] List<int> languageIds)
        {
            logger.LogInformation("Calling the get QuestionsAndOptions endpoint");
            var result = await questionnairerepo.GetQuestionsAndOptionsAsync(count, languageIds);
            logger.LogInformation("Questions and its options have been displayed");
            return Ok(result);
        }

    }
}
