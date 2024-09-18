using Application.Reports.Queries;

namespace Presentation.Controllers
{
    public class ReportsController : ApiBaseController
    {
        [HttpGet("admin-basic")]
        [Authorize(AuthPolicy.CompanyAdmin)]
        public async Task<IActionResult> GetAdminBasicReportAsync()
        {
            return Ok(await Mediator.Send(new GetAdminBasicReportQuery()));
        }

        [HttpGet("employee-basic")]
        [Authorize(AuthPolicy.CompanyAdminOrEmployee)]
        public async Task<IActionResult> GetEmployeeBasicReportAsync()
        {
            return Ok(await Mediator.Send(new GetEmployeeBasicReportQuery()));
        }
    }
}
