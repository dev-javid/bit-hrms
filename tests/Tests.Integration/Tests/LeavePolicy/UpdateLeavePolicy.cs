using Application.LeavePolicies.Commands.SetLeavePolicy;

namespace Tests.Integration.Tests.LeavePolicy
{
    public class UpdateLeavePolicy(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "/api/leave-policy";

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task Should_Return_Data()
        {
            await LoginAsCompanyAdminAsync();
            await FeedDataAsync("Tests/LeavePolicy/UpdateLeavePolicy.sql");
            var command = new SetLeavePolicyCommand
            {
                CasualLeaves = 6,
                EarnedLeavesPerMonth = 2,
                Holidays = 6,
            };
            HttpResponseMessage response;
            using (StringContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, JsoneMediaType))
            {
                response = await Client.PutAsync(Route, content);
            }

            var result = await response.Content.ReadAsStringAsync();
            response.Dispose();
            Approvals.VerifyJson(result);
        }
    }
}
