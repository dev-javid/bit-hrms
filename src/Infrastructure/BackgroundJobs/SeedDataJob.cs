using Domain.Companies;

namespace Infrastructure.BackgroundJobs
{
    internal class SeedDataJob(IDbContext dbContext, IIdentityService identityService)
    {
        public async Task Run()
        {
            if (!await dbContext.Companies.AnyAsync())
            {
                var company = Company.Create("Bit-Xplorer", "dev.javid@outlook.com".ToValueObject<Email>(), "9876543210".ToValueObject<PhoneNumber>(), "Javid Ahmad", 3.ToValueObject<FinancialMonth>());
                await dbContext.Companies.AddAsync(company);
                await dbContext.SaveChangesAsync(default);
            }

            if (!await dbContext.Users.AnyAsync())
            {
                using var transaction = await dbContext.Database.BeginTransactionAsync();
                try
                {
                    var company = await dbContext.Companies.FirstAsync();
                    await identityService.AddSeedDataAsync(company);
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
