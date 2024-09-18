using Application.Holidays.Commands;
using Application.Holidays.Queries;

namespace Presentation.Controllers
{
    public class HolidaysController : ApiBaseController
    {
        [HttpGet]
        [Authorize(AuthPolicy.CompanyAdminOrEmployee)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetHolidaysQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        [Authorize(AuthPolicy.CompanyAdmin)]
        public async Task<IActionResult> AddAsync(AddHolidayCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(new
            {
                Id = id
            });
        }

        [HttpPut("{holidayId}")]
        [Authorize(AuthPolicy.CompanyAdmin)]
        public async Task<IActionResult> UpdateAsync(int holidayId, UpdateHolidayCommand command)
        {
            command.HolidayId = holidayId;
            return Ok(await Mediator.Send(command));
        }
    }
}
