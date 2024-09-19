using System.Reflection;
using NetArchTest.Rules;

namespace Tests.Architecture.Infrastructure
{
    public class InfrastructureProjectReferenceTests
    {
        [Fact]
        public void Infrastructure_Should_Not_Have_Dependency_On_Other_Projects()
        {
            // Arrange
            var assembly = Assembly.Load(ProjectNames.Infrastructure);
            var inaAccessibleProjects = new[]
            {
                ProjectNames.Presentation,
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
