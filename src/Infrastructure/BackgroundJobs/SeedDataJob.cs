namespace Infrastructure.BackgroundJobs
{
    internal class SeedDataJob(IDbContext dbContext, IIdentityService identityService)
    {
        public async Task Run()
        {
            if (!await dbContext.Users.AnyAsync())
            {
                using var transaction = await dbContext.Database.BeginTransactionAsync();
                try
                {
                    var enmail = "admin@example.com".ToValueObject<Email>();
                    var phoneNumber = "9876543210".ToValueObject<PhoneNumber>();

                    await identityService.AddSuperAdminAsync(enmail, phoneNumber, "Password@123");
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
