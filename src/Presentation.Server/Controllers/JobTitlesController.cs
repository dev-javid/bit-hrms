using Application.JobTitles.Commands;
using Application.JobTitles.Queries;

namespace Presentation.Controllers
{
    [Authorize(AuthPolicy.CompanyAdmin)]
    [Route("api/job-titles")]
    public class JobTitlesController : ApiBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetJobTitlesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddJobTitleCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(new
            {
                Id = id
            });
        }

        [HttpPut("{jobTitleId}")]
        public async Task<IActionResult> UpdateAsync(int jobTitleId, UpdateJobTitleCommand command)
        {
            command.JobTitleId = jobTitleId;
            return Ok(await Mediator.Send(command));
        }
    }
}
