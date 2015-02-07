using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAnalysis.DomainObjects.DataSource;
using DataAnalysis.Infrastructure.Data;
using DataAnalysis.Interfaces.Respositories;
using DataAnalysis.Interfaces.Services;

namespace DataAnalysis.Infrastructure.Services
{
    public class DataSourceService : IDataSourceService
    {
        private readonly IDataSourceRepository _dataSourceRepository;

        public DataSourceService(IDataSourceRepository dataSourceRepository)
        {
            _dataSourceRepository = dataSourceRepository;
        }

        /// <summary>
        /// Create a new data source for the provided projectId
        /// </summary>
        public void Create(int projectId, DataSource dataSource)
        {
            _dataSourceRepository.Create(projectId, dataSource.Name, dataSource.Description, dataSource.DataConnectionInfoId);
        }

        /// <summary>
        /// Update the provided data source in our database.
        /// </summary>
        public void UpdateInfo(DataSource dataSource)
        {
            _dataSourceRepository.UpdateInfo(dataSource.Id, dataSource.Name, dataSource.Description, dataSource.DataConnectionInfoId);
        }

        /// <summary>
        /// Get single data source. Optionally load the tables added and the columns of the tables.
        /// </summary>
        /// <param name="dataSourceId">Id of the data source to load.</param>
        /// <param name="loadTables">If yes, tables will also be loaded.</param>
        /// <param name="loadColumns">If yes, columns of the selected tables will also be loaded.</param>
        /// <returns></returns>
        public DataSource GetSingleById(long dataSourceId, bool loadTables, bool loadColumns)
        {
            return _dataSourceRepository.GetSingleById(dataSourceId, loadTables, loadColumns);
        }

        public List<DataConnectionInfo> GetAllConnections()
        {
            return _dataSourceRepository.GetAllConnections();
        }

        /// <summary>
        /// Create a new connection info in our data.
        /// </summary>
        /// <param name="connectionInfo"></param>
        /// <returns></returns>
        public DataConnectionInfo CreateConnection(DataConnectionInfo connectionInfo)
        {
            return _dataSourceRepository.CreateConnection(
                connectionInfo.ServerName, 
                connectionInfo.DatabaseName, 
                connectionInfo.Username, 
                connectionInfo.Password, 
                connectionInfo.ConnectionType);
        }

        public bool TestConnection(DataConnectionInfo info)
        {
            var databaseFacade = DatabaseFacadeFactory.Create(info);
            return databaseFacade.TestConnection();
        }

        /// <summary>
        /// Get tables from the actual data connection source
        /// </summary>
        /// <param name="info">The connection info to get the tables from.</param>
        /// <returns></returns>
        public List<TableInfo> GetTablesFromSource(DataConnectionInfo info)
        {
            var databaseFacade = DatabaseFacadeFactory.Create(info);
            return databaseFacade.GetTables();
        }

        /// <summary>
        /// Add the provided tables to the data source of the provided Id.
        /// We delete all existing tables and set these.
        /// </summary>
        /// <param name="dataSourceId">Id of the data source to add the tables too</param>
        /// <param name="tableInfos">Tables to set.</param>
        public void UpdateTableInfoMappings(long dataSourceId, List<TableInfo> tableInfos)
        {
            _dataSourceRepository.UpdateTableInfoMappings(dataSourceId, tableInfos);
        }
    }
}
