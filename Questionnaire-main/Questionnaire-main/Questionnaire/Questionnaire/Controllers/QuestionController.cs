using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Questionnaire.Model.DbSet;
using Questionnaire.Model.Entity;
using Questionnaire.Repository.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Questionnaire.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    public class QuestionController(
        IGenericRepository<Question> questionrepo,
        IGenericRepository<Language> languagerepo,        
        IMapper mapper, ILogger<QuestionController> logger) : ControllerBase
    {
        // GET: api/question
        [HttpGet]
        [Authorize(Roles = "Admin,HR,Candidate")]
        public async Task<ActionResult<IEnumerable<QuestionDTO>>> GetQuestion()
        {
            logger.LogInformation("Calling the get Question endpoint");
            var question = await questionrepo.GetAllAsync();
            if (question == null)
            {
                logger.LogWarning("Questions not found");
                return NotFound();
            }
            var questionDTO = mapper.Map<IEnumerable<QuestionDTO>>(question);
            logger.LogInformation("successfully fetched Question details");
            return Ok(questionDTO);
        }

        // GET: api/question/2
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,HR")]
        public async Task<ActionResult<QuestionDTO>> GetQuestions(int id)
        {
            logger.LogInformation($"Calling the get Question endpoint by Id: {id}");
            var question = await questionrepo.GetByIdAsync(id);
            if (question == null)
            {
                logger.LogWarning($"Question id: {id} is not valid");
                return NotFound();
            }
            var questionDTO = mapper.Map<QuestionDTO>(question);
            logger.LogInformation($"successfully fetched Question details by Id: {id}");
            return Ok(questionDTO);
        }

        // POST: api/question
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Question>> Create(QuestionDTO questionDTO)
        {
            logger.LogInformation("calling the post Question endpoint");
            var existingQuestion = await questionrepo.GetAllAsync();
            if (existingQuestion.Any(l => l.QuestionText.ToLower() == questionDTO.QuestionText.ToLower()))
            {
                logger.LogWarning("Question already exists: " + questionDTO.QuestionText);
                return Conflict("The Question already exists.");
            }
            if (!Enum.IsDefined(typeof(QuestionCategory), questionDTO.Category))
            {
                return BadRequest("Invalid category.");
            }
            var langentity = await languagerepo.GetByIdAsync(questionDTO.LanguageId);
            if (langentity == null)
            {
                logger.LogWarning($"Language id: {questionDTO.LanguageId} is not found");
                return NotFound();
            }
            questionDTO.LanguageId = langentity.Id;           

            var ques = mapper.Map<Question>(questionDTO);
            await questionrepo.AddAsync(ques);
            var resultDTO = mapper.Map<QuestionDTO>(ques);
            logger.LogInformation("A new question has been inserted successfully");
            return CreatedAtAction(nameof(GetQuestions), new { id = ques.Id }, resultDTO);
        }

        // PUT: api/question/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, QuestionDTO questionDTO)
        {
            logger.LogInformation("calling the put Question endpoint");
            if (id != questionDTO.Id)
            {
                logger.LogWarning("Given id does not match the id in the body");
                return BadRequest();
            }
            var existingQuestion = await questionrepo.GetByIdAsync(id);
            if (existingQuestion == null)
            {
                logger.LogWarning($"values not found at question id: {id}");
                return NotFound();
            }           
            var updatedEntity = mapper.Map(questionDTO, existingQuestion);
            await questionrepo.UpdateAsync(updatedEntity);
            logger.LogInformation($"Fields are updated at the given id: {id}");
            return NoContent();
        }

        // DELETE: api/Question/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            logger.LogInformation("calling the delete Question endpoint");
            var questionToDelete = await questionrepo.GetByIdAsync(id);
            if (questionToDelete == null)
            {
                logger.LogWarning($"No values found at the given id: {id}");
                return NotFound();
            }
            await questionrepo.DeleteAsync(questionToDelete); // delete using entity
            logger.LogInformation("Question deleted successfully");
            return NoContent();
        }       
    }
}
