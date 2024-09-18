using Domain.Employees;

namespace Application.Salaries.Queries
{
    public class GetSalariesQuery : PagedQuery<GetSalariesQuery.Response>
    {
        public int? Month { get; set; }

        public int? Year { get; set; }

        public class Response
        {
            public required int SalaryId { get; set; }

            public required int EmployeeId { get; set; }

            public required decimal Compensation { get; set; }

            public required decimal Amount { get; set; }

            public required int Month { get; set; }

            public required int Year { get; set; }

            public required DateOnly DisbursedOn { get; set; }

            public required IEnumerable<DeductionResponse> Deductions { get; set; }

            public class DeductionResponse
            {
                public required int SalarayDeductionId { get; set; }

                public required decimal Amount { get; set; }

                public required string DeductionType { get; set; }

                public required DateOnly DeductionDate { get; set; }
            }
        }

        internal class Handler(IDbContext dbContext) : IRequestHandler<GetSalariesQuery, PagedResponse<Response>>
        {
            public async Task<PagedResponse<Response>> Handle(GetSalariesQuery request, CancellationToken cancellationToken)
            {
                request.Month ??= DateTime.UtcNow.Month;
                request.Year ??= DateTime.UtcNow.Year;

                return await dbContext.Salaries
                    .Where(x => x.Month.Value == request.Month && x.Year == request.Year)
                    .Select(x => new Response
                    {
                        SalaryId = x.Id,
                        EmployeeId = x.EmployeeId,
                        Amount = x.Amount,
                        Month = x.Month.Value,
                        Year = x.Year,
                        Compensation = x.Employee.Compensations
                                .Where(c => c.EffectiveFrom.Month <= request.Month && c.EffectiveFrom.Year <= request.Year)
                                .OrderByDescending(c => c.EffectiveFrom)
                                .Select(c => c.Amount)
                                .FirstOrDefault(),
                        DisbursedOn = x.CreatedOn.ToDateOnly(),
                        Deductions = x.SalaryDudections.Select(d => new Response.DeductionResponse
                        {
                            SalarayDeductionId = d.Id,
                            Amount = d.Amount,
                            DeductionDate = d.CreatedOn.ToDateOnly(),
                            DeductionType = d.DeductionType.ToString(),
                        })
                    })
                    .OrderBy(x => x.EmployeeId)
                    .ToPagedResponseAsync(request, cancellationToken);
            }
        }
    }
}
