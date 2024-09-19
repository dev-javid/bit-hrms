using System.Reflection;
using NetArchTest.Rules;

namespace Tests.Architecture.Application
{
    public class ApplicationProjectReferenceTests
    {
        [Fact]
        public void Application_Should_Not_Have_Dependency_On_Other_Projects()
        {
            // Arrange
            var assembly = Assembly.Load(ProjectNames.Application);
            var inaAccessibleProjects = new[]
            {
                ProjectNames.Presentation,
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
