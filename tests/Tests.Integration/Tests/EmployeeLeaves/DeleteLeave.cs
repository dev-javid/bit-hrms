namespace Tests.Integration.Tests.EmployeeLeaves
{
    public class DeleteLeave(TestWebApplicationFactory<Program> factory) : IntegrationTest(factory)
    {
        private const string Route = "api/employee-leaves/1";

        [Fact]
        public async Task Should_Return_Data()
        {
            await LoginAsEmployeeAsync();
            await FeedDataAsync("Tests/EmployeeLeaves/DeleteLeave.sql");
            using HttpResponseMessage response = await Client.DeleteAsync(Route);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
