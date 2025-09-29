using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questionnaire.Model.DbSet;
using Questionnaire.Model.Entity;
using Questionnaire.Repository.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Questionnaire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateAnswerController
        (IGenericRepository<CandidateAnswers> candidateanswersrepo,
         IGenericRepository<Question> questionrepo,         
         IMapper mapper,
         ILogger<CandidateAnswerController> logger) : ControllerBase
    {
        // GET: api/CandidateAnswer/my
        [HttpGet("get answers by id")]
        [Authorize(Roles = "Candidate,Admin")]
        public async Task<ActionResult<IEnumerable<CandidateAnswerDTO>>> GetMyAnswers()
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                logger.LogWarning("User ID not found in token");
                return Unauthorized(new { message = "User not authenticated" });
            }

            var allCandidateAnswers = await candidateanswersrepo.GetAllAsync();

            var candidateAnswers = allCandidateAnswers.Where(a => a.CandidateId == userId).OrderBy(a => a.Order);

            if (!candidateAnswers.Any())
            {
                logger.LogWarning($"No answers found for candidateId {userId}");
                return NotFound();
            }

            var result = mapper.Map<IEnumerable<CandidateAnswerDTO>>(candidateAnswers);

            logger.LogInformation($"Returned {result.Count()} answers for candidateId {userId}");

            return Ok(result);
        }


        // GET: api/CandidateAnswer
        [HttpGet]
        [Authorize(Roles = "Admin,HR,Candidate")]
        public async Task<ActionResult<IEnumerable<CandidateAnswerDTO>>> GetAnswer()
        {
            logger.LogInformation("Calling the get CandidateAnswer endpoint");
            var candidateanswer = await candidateanswersrepo.GetAllAsync();
            if (candidateanswer == null || !candidateanswer.Any())
            {
                logger.LogWarning("Candidate Answer Details not found");
                return NotFound();
            }

            // ✅ Sort by Order field before mapping
            var candidateanswerDTO = mapper.Map<IEnumerable<CandidateAnswerDTO>>(
                candidateanswer.OrderBy(a => a.Order)
            );

            logger.LogInformation("Successfully fetched CandidateAnswer details");
            return Ok(candidateanswerDTO);
        }

        // GET: api/CandidateAnswer/2
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,HR")]
        public async Task<ActionResult<CandidateAnswerDTO>> GetAnswers(int id)
        {
            logger.LogInformation($"Calling the get CandidateAnswer detail by Id: {id}");
            var candidateanswer = await candidateanswersrepo.GetByIdAsync(id);
            if (candidateanswer == null)
            {
                logger.LogWarning("Id not found");
                return NotFound();
            }

            var candidateanswerDTO = mapper.Map<CandidateAnswerDTO>(candidateanswer);
            logger.LogInformation($"Successfully fetched CandidateAnswer details by Id: {id}");
            return Ok(candidateanswerDTO);
        }

        // POST: api/CandidateAnswer
        [HttpPost]
        [Authorize(Roles = "Candidate,Admin")]
        public async Task<ActionResult<CandidateAnswers>> Create(CandidateAnswerDTO candidateAnswersDTO)
        {
            logger.LogInformation("Calling the post CandidateAnswer endpoint");

            var userId = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                logger.LogWarning("User ID not found in token");
                return Unauthorized(new { message = "User not authenticated" });
            }

            var questionentity = await questionrepo.GetByIdAsync(candidateAnswersDTO.QuestionId);
            if (questionentity == null)
            {
                logger.LogWarning("QuestionID not found");
                return NotFound(new { message = "QuestionID not found" });
            }

            // Set required values
            var candidateAns = mapper.Map<CandidateAnswers>(candidateAnswersDTO);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            //candidateAns.CandidateId = userId;
            if (role == "Candidate")
            {
                candidateAns.CandidateId = userId; // Use logged-in user

            }
            else
            {
                candidateAns.CandidateId = candidateAnswersDTO.CandidateId;
            }


            candidateAns.CompletedAt = DateTime.Now;
            candidateAns.Order = candidateAnswersDTO.Order;

            await candidateanswersrepo.AddAsync(candidateAns);

            var resultDTO = mapper.Map<CandidateAnswerDTO>(candidateAns);
            logger.LogInformation("Successfully added CandidateAnswer");

            return CreatedAtAction(nameof(GetAnswers), new { id = candidateAns.Id }, resultDTO);
        }

        // PUT: api/CandidateAnswer/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Candidate,Admin")]
        public async Task<IActionResult> UpdateCandidateAnswer(int id, [FromBody] CandidateAnswerDTO candidateAnswersDTO)
        {
            logger.LogInformation($"Calling PUT for CandidateAnswer with ID {id}");

            var existingAnswer = await candidateanswersrepo.GetByIdAsync(id);
            if (existingAnswer == null)
            {
                logger.LogWarning($"CandidateAnswer with ID {id} not found.");
                return NotFound(new { message = $"Answer with ID {id} not found" });
            }

            // Update fields
            existingAnswer.CandidateAnswer = candidateAnswersDTO.Answer;
            existingAnswer.CompletedAt = DateTime.Now;
            existingAnswer.Order = candidateAnswersDTO.Order;

            // Optionally update questionId, candidateId, languageId if needed:
            // existingAnswer.QuestionId = candidateAnswersDTO.QuestionId;
            // existingAnswer.CandidateId = candidateAnswersDTO.CandidateId;
            // existingAnswer.LanguageId = candidateAnswersDTO.LanguageId;

            await candidateanswersrepo.UpdateAsync(existingAnswer);

            logger.LogInformation($"Successfully updated CandidateAnswer with ID {id}");
            return NoContent();
        }



        // DELETE: api/CandidateAnswer/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            logger.LogInformation("Calling the delete CandidateAnswer endpoint");
            var candidateAnswerToDelete = await candidateanswersrepo.GetByIdAsync(id);
            if (candidateAnswerToDelete == null)
            {
                logger.LogWarning($"No values found at the given id: {id}");
                return NotFound();
            }

            await candidateanswersrepo.DeleteAsync(candidateAnswerToDelete);
            logger.LogInformation("CandidateAnswer is deleted successfully");
            return NoContent();
        }
    }
}
