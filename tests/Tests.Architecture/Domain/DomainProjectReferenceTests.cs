using System.Reflection;
using NetArchTest.Rules;

namespace Tests.Architecture.Domain
{
    public class DomainProjectReferenceTests
    {
        [Fact]
        public void Domain_Should_Not_Have_Dependency_On_Other_Projects()
        {
            // Arrange
            var assembly = Assembly.Load(ProjectNames.Domain);
            var inaAccessibleProjects = new[]
            {
                ProjectNames.Presentation,
                ProjectNames.Application,
                ProjectNames.Infrastructure,
                ProjectNames.Database,
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
