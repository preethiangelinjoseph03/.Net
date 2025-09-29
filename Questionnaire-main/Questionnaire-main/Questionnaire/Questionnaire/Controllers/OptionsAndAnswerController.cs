using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Questionnaire.Model.DbSet;
using AutoMapper;
using Questionnaire.Model.Entity;
using Questionnaire.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Questionnaire.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    public class OptionsAndAnswerController(
        IGenericRepository<OptionsAndAnswer> optionsandanswerrepo,
        IGenericRepository<Question> questionrepo,
        IMapper mapper,
        ILogger<OptionsAndAnswerController> logger) : ControllerBase
    {
        // GET: api/optionsandanswer
        [HttpGet]
        [Authorize(Roles = "Candidate,Admin,HR")]
        public async Task<ActionResult<IEnumerable<OptionsAndAnswerDTO>>> GetOptionAndAnswer()
        {
            logger.LogInformation("Calling the get OptionandAnswer endpoint");
            var optionandanswer = await optionsandanswerrepo.GetAllAsync();
            if (optionandanswer == null)
            {
                logger.LogWarning("values not found in the table");
                return NotFound();
            }
            var optionandanswerDTO = mapper.Map<IEnumerable<OptionsAndAnswerDTO>>(optionandanswer);
            logger.LogInformation("Successfully fetched OptionandAnswer details");
            return Ok(optionandanswerDTO);
        }

        // POST: api/optionsandanswer
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<OptionsAndAnswer>> Create(OptionsAndAnswerDTO optionsAndAnswerDTO)
        {
            logger.LogInformation("Calling the create OptionandAnswer endpoint");            
            // Check if the QuestionID exists
            var questionentity = await questionrepo.GetByIdAsync(optionsAndAnswerDTO.QuestionId);
            if (questionentity == null)
            {
                logger.LogWarning($"Question id:{optionsAndAnswerDTO.QuestionId} is not found");
                return NotFound();
            }
            optionsAndAnswerDTO.QuestionId = questionentity.Id;
            var optans = mapper.Map<OptionsAndAnswer>(optionsAndAnswerDTO);
            await optionsandanswerrepo.AddAsync(optans);
            var optdto = mapper.Map<OptionsAndAnswerDTO>(optans);
            logger.LogInformation("Successfully created OptionandAnswer details");
            return CreatedAtAction(nameof(GetOptionAndAnswer), new { id = optans.Id }, optdto);
        }

        //PUT: api/optionsandanswer/3
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> update(int id, OptionsAndAnswerDTO optionsAndAnswerDTO)
        {
            if (id != optionsAndAnswerDTO.Id)
            {
                logger.LogWarning("Given id does not match the id in the body");
                return BadRequest();
            }
            var existingOption = await optionsandanswerrepo.GetByIdAsync(id);
            if (existingOption == null)
            {
                logger.LogWarning($"values not found at OptionAndAnswerId: {id}");
                return NotFound();
            }
            var updatedEntity = mapper.Map(optionsAndAnswerDTO, existingOption);           
            await optionsandanswerrepo.UpdateAsync(updatedEntity);
            logger.LogInformation($"Successfully Updated the OptionsandAnswer Details at the id: {id}");
            return NoContent();
        }

        // DELETE: api/optionsandanswer/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            logger.LogInformation("calling the delete optionsandanswer endpoint");
            var optionsandanswer = await optionsandanswerrepo.GetByIdAsync(id);
            if (optionsandanswer == null)
            {
                logger.LogWarning($"No values found at the given id: {id}");
                return NotFound();
            }
            await optionsandanswerrepo.DeleteAsync(optionsandanswer); // delete using entity
            logger.LogInformation("optionsandanswer deleted successfully");
            return NoContent();
        }
    }
}
