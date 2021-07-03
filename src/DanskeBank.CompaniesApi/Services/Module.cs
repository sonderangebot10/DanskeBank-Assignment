
using Autofac;
using DanskeBank.Application.Services;

namespace DanskeBank.CompaniesApi.Services
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SsnService>()
                .As<ISsnService>()
                .InstancePerLifetimeScope();
        }
    }
}
