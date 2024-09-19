using System.Reflection;
using Microsoft.AspNetCore.Authorization;

namespace Tests.Unit.Presentation
{
    public abstract class ControllerTests<T> where T : ControllerBase, new()
    {
        public static void RunAuthorizationTest(string apiMethod, string authPolicy)
        {
            // Arrange
            var controllerType = typeof(T);
            var methodInfo = controllerType.GetMethod(apiMethod)!;

            // Act
            var authorizeAttributes = methodInfo.GetCustomAttributes<AuthorizeAttribute>();
            if (!authorizeAttributes.Any())
            {
                authorizeAttributes = controllerType.GetCustomAttributes<AuthorizeAttribute>();
            }

            var allowAnonymousAttribute = methodInfo.GetCustomAttribute<AllowAnonymousAttribute>();
            allowAnonymousAttribute ??= controllerType.GetCustomAttribute<AllowAnonymousAttribute>();

            // Assert
            if (authPolicy == AuthPolicy.AllowAnonymous)
            {
                allowAnonymousAttribute.Should().NotBeNull();
                authorizeAttributes.Count().Should().Be(0);
            }
            else
            {
                allowAnonymousAttribute.Should().BeNull();
                authorizeAttributes.Should().NotBeNull();
                authorizeAttributes.Count().Should().NotBe(0);
                var expectedAttribute = authorizeAttributes.FirstOrDefault(x => x.Policy == authPolicy);
                expectedAttribute.Should().NotBeNull();
            }
        }

        public abstract void Authorization_Tests(string methodName, string expectedPolicy);
    }
}
