using System.Reflection;
using System.Runtime.CompilerServices;
using Application.Common.Validators;
using FluentValidation;
using MediatR;
using NetArchTest.Rules;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Tests.Architecture.Application
{
    public class MediatRRequestTests
    {
        [Fact]
        public void Each_Command_Or_Query_Should_Have_A_Nested_Handler()
        {
            //Arrange
            var assembly = Assembly.Load(ProjectNames.Application);
            Type targetType = typeof(IBaseRequest);
            IEnumerable<Type> commandsAndQueries = Types.InAssembly(assembly)
                .That()
                .AreClasses()
                .GetTypes()
                .Where(t => targetType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

            //Act
            var failingTypes = new List<Type>();
            foreach (Type type in commandsAndQueries)
            {
                var handler = Array
                    .Find(type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic), x => x.Name.ToLower().EndsWith("handler"));

                if (handler is null)
                {
                    failingTypes.Add(type);
                }
            }

            //Assert
            failingTypes.Should().BeEmpty();
        }

        [Fact]
        public void Nested_Handlers_Should_Be_Internal()
        {
            //Arrange
            var assembly = Assembly.Load(ProjectNames.Application);
            Type targetType = typeof(IBaseRequest);
            IEnumerable<Type> nestedTypes = Types.InAssembly(assembly)
                .That()
                .AreClasses()
                .GetTypes()
                .Where(t => targetType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .SelectMany(x => x.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic));

            //Act
            var failingTypes = new List<Type>();
            foreach (Type nestedType in nestedTypes)
            {
                if (nestedType.Name == "Handler" && nestedType.IsPublic)
                {
                    failingTypes.Add(nestedType);
                }
            }

            //Assert
            failingTypes.Should().BeEmpty();
        }

        [Fact]
        public void Nested_Handlers_Should_Be_Named_As_Handler()
        {
            //Arrange
            var assembly = Assembly.Load(ProjectNames.Application);
            Type targetType = typeof(IBaseRequest);
            IEnumerable<Type> nestedTypes = Types.InAssembly(assembly)
                .That()
                .AreClasses()
                .GetTypes()
                .Where(t => targetType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .SelectMany(x => x.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic));

            //Act
            var failingTypes = new List<Type>();
            foreach (Type nestedType in nestedTypes)
            {
                if (nestedType.Name.ToLower().EndsWith("handler") && nestedType.Name != "Handler")
                {
                    failingTypes.Add(nestedType);
                }
            }

            //Assert
            failingTypes.Should().BeEmpty();
        }

        [Fact]
        public void Nested_Responses_Should_Be_Named_As_Response()
        {
            //Arrange
            var assembly = Assembly.Load(ProjectNames.Application);
            Type targetType = typeof(IBaseRequest);
            IEnumerable<Type> nestedTypes = Types.InAssembly(assembly)
                .That()
                .AreClasses()
                .GetTypes()
                .Where(t => targetType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .SelectMany(x => x.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic));

            //Act
            var failingTypes = new List<Type>();
            foreach (Type nestedType in nestedTypes)
            {
                if (nestedType.Name.ToLower().EndsWith("response") && nestedType.Name != "Response")
                {
                    failingTypes.Add(nestedType);
                }
            }

            //Assert
            failingTypes.Should().BeEmpty();
        }

        [Fact]
        public void Responses_Public_Properties_Should_Be_Marked_Required()
        {
            //Arrange
            var assembly = Assembly.Load(ProjectNames.Application);
            Type targetType = typeof(IBaseRequest);
            IEnumerable<Type> nestedTypes = Types.InAssembly(assembly)
                .That()
                .AreClasses()
                .GetTypes()
                .Where(t => targetType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .SelectMany(x => x.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic));

            //Act
            var failingTypes = new List<Type>();
            foreach (Type nestedType in nestedTypes)
            {
                if (nestedType.Name.ToLower().EndsWith("response") && nestedType.Name == "Response")
                {
                    var properiesNotRequired = nestedType
                       .GetProperties()
                       .Where(prop => prop.IsPubliclyReadable() && prop.GetCustomAttribute<RequiredMemberAttribute>() == null)
                       .ToList();

                    if (properiesNotRequired.Any())
                    {
                        failingTypes.Add(nestedType);
                    }
                }
            }

            //Assert
            failingTypes.Should().BeEmpty();
        }

        [Fact]
        public void Command_And_Query_Public_Properties_Should_Not_Be_Marked_Required()
        {
            //Arrange
            var assembly = Assembly.Load(ProjectNames.Application);
            Type targetType = typeof(IBaseRequest);
            IEnumerable<Type> commandsAndQueries = Types.InAssembly(assembly)
                .That()
                .AreClasses()
                .GetTypes()
                .Where(t => targetType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

            //Act
            var failingTypes = new List<Type>();
            foreach (Type type in commandsAndQueries)
            {
                var requiredPublicProeprties = type
                       .GetProperties()
                       .Where(prop => prop.IsPubliclyWritable() && prop.GetCustomAttribute<RequiredMemberAttribute>() != null)
                       .ToList();

                if (requiredPublicProeprties.Any())
                {
                    failingTypes.Add(type);
                }
            }

            //Assert
            failingTypes.Should().BeEmpty();
        }

        [Fact]
        public void Nested_Validators_Should_Be_Named_As_Validator()
        {
            //Arrange
            var assembly = Assembly.Load(ProjectNames.Application);
            IEnumerable<Type> validators = Types.InAssembly(assembly)
                .That()
                .AreClasses()
                .GetTypes()
                .Where(type => !type.IsAbstract && type.BaseType != null && IsSubclassOfRawGeneric(type.BaseType, typeof(AbstractValidator<>)))
                .SelectMany(x => x.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic))
                .Where(x => !x.FullName!.Contains("Application.Common.Validators"));

            //Act
            var failingTypes = new List<Type>();
            foreach (Type validator in validators)
            {
                if (validator.IsNested && !validator.FullName!.Contains("+Validator+<>"))
                {
                    failingTypes.Add(validator);
                }
            }

            //Assert
            failingTypes.Should().BeEmpty();
        }

        private static bool IsSubclassOfRawGeneric(Type toCheck, Type generic)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }

                toCheck = toCheck.BaseType!;
            }

            return false;
        }
    }
}
