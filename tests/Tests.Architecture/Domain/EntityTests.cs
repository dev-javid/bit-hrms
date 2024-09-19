using System.Reflection;
using Domain.Common;
using NetArchTest.Rules;

namespace Tests.Architecture.Domain
{
    public class EntityTests
    {
        [Fact]
        public void Entities_ShouldOnlyHave_PrivateConstructors()
        {
            RunTest<int>();
            RunTest<Guid>();

            static void RunTest<T>() where T : struct
            {
                //Arrange
                var assembly = Assembly.Load(ProjectNames.Domain);
                IEnumerable<Type> entityTypes = Types.InAssembly(assembly)
                    .That()
                    .Inherit(typeof(Entity<T>))
                    .GetTypes();

                //Act
                var failingTypes = new List<Type>();
                foreach (Type entityType in entityTypes)
                {
                    ConstructorInfo[] constructors = entityType
                        .GetConstructors(BindingFlags.Public | BindingFlags.Instance);

                    if (constructors.Length != 0)
                    {
                        failingTypes.Add(entityType);
                    }
                }

                //Assert
                failingTypes.Should().BeEmpty();
            }
        }
    }
}
