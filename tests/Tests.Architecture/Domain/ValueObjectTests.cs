using System.Reflection;
using Domain.Common;
using NetArchTest.Rules;

namespace Tests.Architecture.Domain
{
    public class ValueObjectTests
    {
        [Fact]
        public void ValueObjects_ShouldOnlyHave_PrivateConstructors()
        {
            //Arrange
            var assembly = Assembly.Load(ProjectNames.Domain);
            IEnumerable<Type> valueObjects = Types.InAssembly(assembly)
                .That()
                .Inherit(typeof(ValueObject))
                .GetTypes();

            //Act
            var failingTypes = new List<Type>();
            foreach (Type valueObject in valueObjects)
            {
                ConstructorInfo[] constructors = valueObject
                    .GetConstructors(BindingFlags.Public | BindingFlags.Instance);

                if (constructors.Length != 0)
                {
                    failingTypes.Add(valueObject);
                }
            }

            //Assert
            failingTypes.Should().BeEmpty();
        }
    }
}
