namespace Tests.Integration.Fixture
{
    [CollectionDefinition("TestCollection", DisableParallelization = true)]
    public class TestCollection
        : ICollectionFixture<TestWebApplicationFactory<Program>>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.

        // A collection groups together all test classes decorated with
        // a Collection attribute having the same name parameter.

        // The collection fixture (FunctionalTestWebApplicationFactory<Startup>)
        // will be created once before running any test in the collection,
        // and destroyed after all tests in the collection have been run.
    }
}
