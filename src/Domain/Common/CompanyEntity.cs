using Domain.Companies;

namespace Domain.Common
{
    public class CompanyEntity<TId> : Entity<TId> where TId : struct
    {
        public int CompanyId { get; }

        public Company Company { get; } = null!;
    }
}
