using System.Reflection;
using NetArchTest.Rules;

namespace Tests.Architecture.Database
{
    public class DomainProjectReferenceTests
    {
        [Fact]
        public void Database_Should_Not_Have_Dependency_On_Other_Projects()
        {
            // Arrange
            var assembly = Assembly.Load(ProjectNames.Domain);
            var inaAccessibleProjects = new[]
            {
                ProjectNames.Presentation,
                ProjectNames.Application,
                ProjectNames.Infrastructure,
                ProjectNames.Domain,
            };

            // Act
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(inaAccessibleProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }
    }
}
