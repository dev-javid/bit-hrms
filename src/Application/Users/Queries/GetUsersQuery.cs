namespace Application.Users.Queries
{
    public class GetUsersQuery : PagedQuery<GetUsersQuery.Response>
    {
        public class Response
        {
            public required int UserId { get; set; }

            public required string? Email { get; set; }

            public required string? PhoneNumber { get; set; }

            public required IEnumerable<object> Claims { get; set; }
        }

        internal class Handler(IDbContext dbContext) : IRequestHandler<GetUsersQuery, PagedResponse<Response>>
        {
            public async Task<PagedResponse<Response>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
            {
                return await dbContext.Users
                   .Select(x => new Response
                   {
                       UserId = x.Id,
                       Email = x.Email,
                       PhoneNumber = x.PhoneNumber,
                       Claims = x.Claims
                          .Select(c => new
                          {
                              c.ClaimType,
                              c.ClaimValue
                          })
                   })
                   .OrderBy(x => x.UserId)
                   .ToPagedResponseAsync(request, cancellationToken);
            }
        }
    }
}
