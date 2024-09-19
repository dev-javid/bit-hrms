using Domain.Attendance;
using Domain.Companies;
using Domain.Employees;
using Domain.Finance;
using Domain.Identity;
using Domain.Salaries;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Common.Abstract
{
    public interface IDbContext : IDisposable
    {
        public DbSet<User> Users { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<LeavePolicy> LeavePolicies { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<JobTitle> JobTitles { get; set; }

        public DbSet<Holiday> Holidays { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeLeave> EmployeeLeaves { get; set; }

        public DbSet<Salary> Salaries { get; set; }

        public DbSet<EmployeeDocument> EmployeeDocuments { get; set; }

        public DbSet<ClockInOutTiming> ClockInOutTimings { get; set; }

        public DbSet<AttendanceRegularization> AttendanceRegularizations { get; set; }

        public DbSet<Compensation> Compensations { get; set; }

        public DbSet<IncomeSource> IncomeSources { get; set; }

        public DbSet<Income> Incomes { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DatabaseFacade Database { get; }

        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        public DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
