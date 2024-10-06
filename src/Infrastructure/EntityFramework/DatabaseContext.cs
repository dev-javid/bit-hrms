using System.Data;
using System.Linq.Expressions;
using Domain.Attendance;
using Domain.Companies;
using Domain.Employees;
using Domain.Finance;
using Domain.Salaries;
using Infrastructure.EntityFramework.Configuration;
using Infrastructure.EntityFramework.Encryption;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.EntityFramework;

internal class DatabaseContext(DbContextOptions<DatabaseContext> options, ICurrentUser currentUser, IEncryptionProvider encryptionProvider, IWebHostEnvironment environment) : IdentityDbContext<User, IdentityRole<int>, int>(options), IDbContext
{
    private readonly ICurrentUser _currentUser = currentUser;

    public DbSet<DomainEvent> DomainEvents { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<Company> Companies { get; set; }

    public DbSet<LeavePolicy> LeavePolicies { get; set; }

    public DbSet<Department> Departments { get; set; }

    public DbSet<JobTitle> JobTitles { get; set; }

    public DbSet<Holiday> Holidays { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<EmployeeLeave> EmployeeLeaves { get; set; }

    public DbSet<EmployeeDocument> EmployeeDocuments { get; set; }

    public DbSet<Salary> Salaries { get; set; }

    public DbSet<AttendanceRegularization> AttendanceRegularizations { get; set; }

    public DbSet<Compensation> Compensations { get; set; }

    public DbSet<ClockInOutTiming> ClockInOutTimings { get; set; }

    public DbSet<IncomeSource> IncomeSources { get; set; }

    public DbSet<Income> Incomes { get; set; }

    public DbSet<Expense> Expenses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        AddCompanyGlobalQueryFilter(builder);
        AddPrimaryKeyMapping(builder);
        AddCompanCompanyIdMapping(builder);
        ConfigureShadowProperties(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        builder.UseEncryption(encryptionProvider);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging(environment.IsDevelopment());
        optionsBuilder.LogTo(Serilog.Log.Debug);
    }

    private static void ConfigureShadowProperties(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        var entityTypes = typeof(User).Assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.IsSubclassOf(typeof(Entity<int>)))
            .ToList();

        var identityClasses = new[]
        {
            typeof(User),
            typeof(IdentityRole<int>),
            typeof(IdentityRoleClaim<int>),
            typeof(IdentityUserClaim<int>),
            typeof(IdentityUserLogin<int>),
            typeof(IdentityUserRole<int>),
            typeof(IdentityUserToken<int>),
        };

        entityTypes.AddRange(identityClasses);

        foreach (var configurationInstance in entityTypes
                     .Select(entityType =>
                         typeof(ShadowPropertyConfiguration<>).MakeGenericType(entityType))
                     .Select(Activator.CreateInstance))
        {
            builder.ApplyConfiguration(configurationInstance as dynamic);
        }
    }

    private void AddCompanyGlobalQueryFilter(ModelBuilder builder)
    {
        AddFilter<int>(builder);
        AddFilter<Guid>(builder);

        void AddFilter<T>(ModelBuilder builder) where T : struct
        {
            Expression<Func<CompanyEntity<T>, bool>> filterExpr = e => e.CompanyId == _currentUser.CompanyId;

            builder.Model.GetEntityTypes().ToList().ForEach(mutableEntityType =>
            {
                if (mutableEntityType.ClrType.IsAssignableTo(typeof(CompanyEntity<T>)))
                {
                    var parameter = Expression.Parameter(mutableEntityType.ClrType);
                    var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters[0], parameter, filterExpr.Body);
                    var lambdaExpression = Expression.Lambda(body, parameter);
                    mutableEntityType.SetQueryFilter(lambdaExpression);
                }
            });
        }
    }

    private void AddCompanCompanyIdMapping(ModelBuilder builder)
    {
        AddMapping<int>(builder);
        AddMapping<Guid>(builder);

        void AddMapping<T>(ModelBuilder builder) where T : struct
        {
            builder.Model.GetEntityTypes().ToList().ForEach(mutableEntityType =>
            {
                if (mutableEntityType.ClrType.IsAssignableTo(typeof(CompanyEntity<T>)))
                {
                    builder.Entity(mutableEntityType.ClrType).Property("CompanyId")
                        .HasColumnName("company_id");
                }
            });
        }
    }

    private void AddPrimaryKeyMapping(ModelBuilder builder)
    {
        AddMapping<int>(builder);
        AddMapping<Guid>(builder);

        void AddMapping<T>(ModelBuilder builder) where T : struct
        {
            builder.Model.GetEntityTypes().ToList().ForEach(mutableEntityType =>
            {
                if (mutableEntityType.ClrType.IsAssignableTo(typeof(Entity<T>)))
                {
                    builder.Entity(mutableEntityType.ClrType)
                        .Property("Id")
                        .HasColumnName("id");
                }
            });
        }
    }
}
