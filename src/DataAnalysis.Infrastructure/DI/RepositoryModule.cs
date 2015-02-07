using Autofac;
using DataAnalysis.Infrastructure.Repositories;
using DataAnalysis.Interfaces.Respositories;

namespace DataAnalysis.Infrastructure.DI
{
    public class RepositoryModule : Module
    {
        private readonly string _connectionString;

        public RepositoryModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(i => new DataSourceRepository()).As<IDataSourceRepository>().InstancePerRequest();
            builder.Register(i => new ProjectRepository(i.Resolve<IDataSourceRepository>())).As<IProjectRepository>().InstancePerRequest();

            base.Load(builder);
        }
    }
}
