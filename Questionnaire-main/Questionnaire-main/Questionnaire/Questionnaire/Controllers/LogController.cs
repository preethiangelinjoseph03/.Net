using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questionnaire.Model.Entity;
using AutoMapper;
using Questionnaire.Model.DbSet;
using Questionnaire.Repository.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Questionnaire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController(IGenericRepository<Logs> logs, IMapper mapper, ILogger<LogsController> logger) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<LogsDTO>>> Getlogs()
        {
            logger.LogInformation("LogInformation: Calling the get logs endpoint");

            var getlogs = await logs.GetAllAsync();
            if (getlogs == null) { return NotFound(); }
            var logsDTO = mapper.Map<IEnumerable<LogsDTO>>(getlogs);
            logger.LogInformation("LogInformation: Successufully fetched logs details");
            return Ok(logsDTO);
        }

        // GET: api/question/2 
        [HttpGet("{date}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<LogsDTO>>> GetLogsByDate(DateTime date)
        {
            logger.LogInformation($"Calling the get logs endpoint by date: {date}", date.ToShortDateString());
            var logDate = await logs.GetAllAsync();
            var filteredLogs = logDate.Where(log => log.TimeStamp.Date == date.Date).ToList();
            if (!filteredLogs.Any())
            {
                logger.LogWarning($"No Logs Found on Date:{date}", date.ToShortTimeString());
                return NotFound($"No logs found for {date:yyyy-MM-dd}");
            }
            var logsDTO = mapper.Map<IEnumerable<LogsDTO>>(filteredLogs);
            logger.LogInformation($"Successfully fetched logs for date: {date}");
            return Ok(logsDTO);
        }
    }
}
