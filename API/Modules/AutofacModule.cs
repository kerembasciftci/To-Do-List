using Autofac;
using Business.Mapping;
using Business.Services.Abstract;
using Business.Services.Concrete;
using Core.DataAccess.Repositories;
using Core.Services;
using Core.UnitOfWorks;
using DataAccess.Contexts;
using DataAccess.Repositories.Concrete.EntityFramework;
using System.Reflection;
using Module = Autofac.Module;

namespace API.Modules
{
    public class AutofacModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfGenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();
            builder.RegisterType<LoggerManager>().As<ILoggerService>().InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));


            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith
            ("Dal")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith
            ("Manager")).AsImplementedInterfaces().InstancePerLifetimeScope();

            //InstancePerLifetimeScope = Scope
            //InstancePerDependency = transient
        }
    }
}
