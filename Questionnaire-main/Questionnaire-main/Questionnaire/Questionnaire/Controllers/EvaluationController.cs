using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questionnaire.Model.DbSet;
using Questionnaire.Model.Entity;
using Questionnaire.Repository.Interface;

namespace Questionnaire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationController
        (IGenericRepository<AnswerEvaluation> evaluationrepo,
         IGenericRepository<CandidateAnswers> candidateanswerrepo,        
         IGenericRepository<Question> questionrepo,
         IMapper mapper,
         ILogger<CandidateAnswerController> logger) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        public async Task<ActionResult<IEnumerable<AnswerEvaluationDTO>>> GetEvaluation()
        {
            logger.LogInformation("Calling the get evaluation endpoint");
            var evaluation = await evaluationrepo.GetAllAsync();
            if (evaluation == null)
            {
                logger.LogWarning("evaluation not found");
                return NotFound();
            }
            var EvaluationDTO = mapper.Map<IEnumerable<AnswerEvaluationDTO>>(evaluation);
            logger.LogInformation("successfully fetched Evaluation details");
            return Ok(EvaluationDTO);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        public async Task<ActionResult<AnswerEvaluation>> Create(AnswerEvaluationDTO answerevaluationDTO)
        {
            logger.LogInformation("Calling the create evaluation endpoint");
            // Check if the QuestionID exists
           
            var candidateAnswerEntity = await candidateanswerrepo.GetByIdAsync(answerevaluationDTO.CandidateanswerId);
            if (candidateAnswerEntity == null)
            {
                logger.LogWarning($"CandidateAnswer id: {answerevaluationDTO.CandidateanswerId} not found");
                return NotFound($"CandidateAnswer id: {answerevaluationDTO.CandidateanswerId} not found");
            }           
            var eval = mapper.Map<AnswerEvaluation>(answerevaluationDTO);
            await evaluationrepo.AddAsync(eval);
            var evaldto = mapper.Map<AnswerEvaluationDTO>(eval);
            logger.LogInformation("Successfully created OptionandAnswer details");
            return CreatedAtAction(nameof(GetEvaluation), new { id = eval.Id }, evaldto);
        }
    }
}
