using Autofac;
using Microsoft.EntityFrameworkCore;

namespace DanskeBank.Infrastructure.Data.Sql.Companies
{
    public class Module : Autofac.Module
    {
        public string DBName { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseInMemoryDatabase(DBName);
            optionsBuilder.EnableSensitiveDataLogging(true);

            builder.RegisterType<Context>()
              .WithParameter(new TypedParameter(typeof(DbContextOptions), optionsBuilder.Options))
              .SingleInstance();

            builder.RegisterDanskeServerInfrastructureAssembly(GetType());
        }
    }
}
