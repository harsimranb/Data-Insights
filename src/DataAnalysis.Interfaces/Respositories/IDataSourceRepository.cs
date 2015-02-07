using System.Collections.Generic;
using DataAnalysis.DomainObjects.DataSource;

namespace DataAnalysis.Interfaces.Respositories
{
    public interface IDataSourceRepository
    {
        void Create(int projectId, string name, string description, int connectionInfoId);
        void UpdateInfo(long dataSourceId, string name, string description, int connectionInfoId);
        DataSource GetSingleById(long dataSourceId, bool loadTables, bool loadColumns);
        List<DataConnectionInfo> GetAllConnections();
        List<DataSource> GetAllByProjectId(int projectId, bool loadTables, bool loadColumns);
        DataConnectionInfo CreateConnection(string serverName, string databaseName, string username, string password, DataConnectionType type);
        void UpdateTableInfoMappings(long dataSourceId, List<TableInfo> tableInfos);
        List<TableInfo> GetTableInfosByDataSourceId(long dataSourceId, bool loadColumnInfos);
        List<ColumnInfo> GetColumnInfosByTableInfoId(long tableInfoId);
    }
}