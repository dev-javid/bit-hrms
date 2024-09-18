using Application.Users.Commands;
using Application.Users.Queries;

namespace Presentation.Controllers
{
    [Authorize(AuthPolicy.CompanyAdmin)]
    public class UsersController : ApiBaseController
    {
        [HttpPost]
        public async Task<IActionResult> AddAsync(AddUserCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]GetUsersQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
