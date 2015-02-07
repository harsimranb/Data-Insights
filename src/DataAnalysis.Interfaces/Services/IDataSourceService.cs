using System.Collections.Generic;
using DataAnalysis.DomainObjects.DataSource;

namespace DataAnalysis.Interfaces.Services
{
    public interface IDataSourceService
    {
        void Create(int projectId, DataSource dataSource);
        void UpdateInfo(DataSource dataSource);
        DataSource GetSingleById(long dataSourceId, bool loadTables, bool loadColumns);
        List<DataConnectionInfo> GetAllConnections();
        DataConnectionInfo CreateConnection(DataConnectionInfo connectionInfo);
        bool TestConnection(DataConnectionInfo connectionInfo);
        List<TableInfo> GetTablesFromSource(DataConnectionInfo info);
        void UpdateTableInfoMappings(long dataSourceId, List<TableInfo> tableInfos);
    }
}