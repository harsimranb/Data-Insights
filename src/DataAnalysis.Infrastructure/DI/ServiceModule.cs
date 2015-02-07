using Autofac;
using DataAnalysis.Infrastructure.Services;
using DataAnalysis.Interfaces.Respositories;
using DataAnalysis.Interfaces.Services;

namespace DataAnalysis.Infrastructure.DI
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(i => new ProjectService(i.Resolve<IProjectRepository>())).As<IProjectService>().InstancePerRequest();
            builder.Register(i => new DataSourceService(i.Resolve<IDataSourceRepository>())).As<IDataSourceService>().InstancePerRequest();

            base.Load(builder);
        }
    }
}
