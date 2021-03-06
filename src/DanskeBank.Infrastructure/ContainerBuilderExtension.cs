using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using System;
using System.Linq;

namespace DanskeBank.Infrastructure
{
    /// <summary>
    /// the container builder extension for the infrastructure library.
    /// </summary>
    public static class ContainerBuilderExtension
    {
        private static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>
            RegisterDanskeServerInfrastructureAssembly(this ContainerBuilder builder, Func<Type, bool> filter)
        {
            var t = typeof(InfrastructureException);

            return builder.RegisterAssemblyTypes(t.Assembly)
                .Where(filter)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        /// <summary>
        /// registers all the types belonging to the infrastructure library as implemented interface instances.
        /// </summary>
        /// <param name="builder">the builder container</param>
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>
            RegisterDanskeServerInfrastructureAssembly(this ContainerBuilder builder)
        {
            var ns = typeof(InfrastructureException).Namespace ?? string.Empty;
            return builder.RegisterDanskeServerInfrastructureAssembly(
                t => t.IsPublic
                     && t.Namespace != null
                     && t.Namespace.Contains(ns));
        }

        /// <summary>
        /// registers all types belonging to the infrastructure library as implemented interface instances, filtering on the required types.
        /// </summary>
        /// <param name="builder">the builder container</param>
        /// <param name="requiredTypes">the required types for the specific module target</param>
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>
            RegisterDanskeServerInfrastructureAssembly(this ContainerBuilder builder, params Type[] requiredTypes)
        {
            return builder.RegisterDanskeServerInfrastructureAssembly(requiredTypes.Select(t => t.Namespace).ToArray());
        }

        private static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>
            RegisterDanskeServerInfrastructureAssembly(this ContainerBuilder builder, params string[] nameSpaces)
        {
            if (nameSpaces == null || nameSpaces.Length == 0)
            {
                return builder.RegisterDanskeServerInfrastructureAssembly();
            }

            return builder.RegisterDanskeServerInfrastructureAssembly(
                t => t.IsPublic
                     && t.Namespace != null
                     && nameSpaces.Any(ns => t.Namespace.Contains(ns)));
        }
    }
}
