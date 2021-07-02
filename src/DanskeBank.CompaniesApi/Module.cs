
using Autofac;

namespace DanskeBank.CompaniesApi
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // register all types of settings
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
