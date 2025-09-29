using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questionnaire.Model.DbSet;
using Questionnaire.Model.Entity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Questionnaire.Repository.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Questionnaire.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    public class LanguageController(IGenericRepository<Language> languagerepo, ILogger<LanguageController> logger,
        IMapper mapper) : ControllerBase
    {
        // GET: api/questionnaires/language
        [HttpGet]
        [Authorize(Roles = "Admin,HR,Candidate")]
        public async Task<ActionResult<IEnumerable<LanguageDTO>>> GetLanguage()
        {
            logger.LogInformation("LogInformation: Calling the get language endpoint");
            var languages = await languagerepo.GetAllAsync();
            if (languages == null)
            {
                logger.LogWarning("Languages not found");
                return NotFound();
            }
            var languageDTO = mapper.Map<IEnumerable<LanguageDTO>>(languages);
            logger.LogInformation("LogInformation: Successufully fetched language details");
            return Ok(languageDTO);
        }

        // POST: api/questionnaires/Language
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<LanguageDTO>> AddLanguage(LanguageDTO languageDTO)
        {
            logger.LogInformation("LogInformation: Calling the post language endpoint");            
            var existingLanguage = await languagerepo.GetAllAsync(); 
            if (existingLanguage.Any(l=>l.LanguageName.ToLower()==languageDTO.LanguageName.ToLower()))
            {
                logger.LogWarning("Language already exists: " + languageDTO.LanguageName);
                return Conflict("The language already exists.");
            }
            var lang = mapper.Map<Language>(languageDTO);
            await languagerepo.AddAsync(lang);
            var resultDTO = mapper.Map<LanguageDTO>(lang);
            logger.LogInformation("LogInformation: Successufully inserted language values");
            return CreatedAtAction(nameof(GetLanguage), new { id = lang.Id }, resultDTO);
        }

        // PUT: api/questionnaires/Language
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> update(int id, LanguageDTO languageDTO)
        {
            if (id != languageDTO.Id)
            {
                logger.LogWarning("Given id does not match the id in the body");
                return BadRequest();

            }
            var existingOption = await languagerepo.GetByIdAsync(id);
            if (existingOption == null)
            {
                logger.LogWarning($"values not found at LanguageId: {id}");
                return NotFound();
            }
            var updatedEntity = mapper.Map(languageDTO, existingOption);           
            await languagerepo.UpdateAsync(updatedEntity);
            logger.LogInformation($"Successfully Updated the Language Details at the id: {id}");
            return NoContent();
        }

        // DELETE: api/Language/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            logger.LogInformation("calling the delete Language endpoint");
            var LanguageToDelete = await languagerepo.GetByIdAsync(id);
            if (LanguageToDelete == null)
            {
                logger.LogWarning($"No values found at the given id: {id}");
                return NotFound();
            }
            await languagerepo.DeleteAsync(LanguageToDelete); // delete using entity
            logger.LogInformation("Language deleted successfully");
            return NoContent();
        }
    }
}
