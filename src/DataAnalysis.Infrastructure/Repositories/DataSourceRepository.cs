using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataAnalysis.DomainObjects.DataSource;
using DataAnalysis.DomainObjects.Exceptions;
using DataAnalysis.Infrastructure.Data;
using DataAnalysis.Infrastructure.Extensions;
using DataAnalysis.Infrastructure.Framework;
using DataAnalysis.Interfaces.Data;
using DataAnalysis.Interfaces.Respositories;
using FastMember;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace DataAnalysis.Infrastructure.Repositories
{
    public class DataSourceRepository : IDataSourceRepository
    {
        #region Data Source

        public void Create(int projectId, string name, string description, int connectionInfoId)
        {
            try
            {
                var db = SqlDatabaseFactory.Create(Constants.ConnectionStrings.Core);

                var command = db.GetStoredProcCommand("[dbo].[pr_CreateDataSource]");
                db.AddInParameter(command, "@name", SqlDbType.NVarChar, name);
                db.AddInParameter(command, "@description", SqlDbType.NVarChar, description);
                db.AddInParameter(command, "@data_connection_info_id", SqlDbType.Int, connectionInfoId);
                db.AddInParameter(command, "@project_id", SqlDbType.Int, projectId);

                db.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw new BasicDataException(string.Format("An unexpected error occurred while creating a data source. ERROR: {0}", ex.Message), ex);
            }
        }

        public void UpdateInfo(long dataSourceId, string name, string description, int connectionInfoId)
        {
            try
            {
                var db = SqlDatabaseFactory.Create(Constants.ConnectionStrings.Core);

                var command = db.GetStoredProcCommand("[dbo].[pr_UpdateDataSourceInfo]");
                db.AddInParameter(command, "@data_source_id", SqlDbType.BigInt, dataSourceId);
                db.AddInParameter(command, "@name", SqlDbType.NVarChar, name);
                db.AddInParameter(command, "@description", SqlDbType.NVarChar, description);
                db.AddInParameter(command, "@data_connection_info_id", SqlDbType.Int, connectionInfoId);

                db.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw new BasicDataException(string.Format("An unexpected error occurred while update the data source. ERROR: {0}", ex.Message), ex);
            }
        }

        public DataSource GetSingleById(long dataSourceId, bool loadTables, bool loadColumns)
        {
            try
            {
                var db = SqlDatabaseFactory.Create(Constants.ConnectionStrings.Core);
                var command = db.GetStoredProcCommand("[dbo].[pr_GetDataSourceById]");

                db.AddInParameter(command, "@data_source_id", SqlDbType.BigInt, dataSourceId);

                using (var result = db.ExecuteDataSet(command))
                {
                    if (result.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("The data source could not be found. It might've been deleted by another user.");
                    }

                    var dataSourceRow = result.Tables[0].Rows[0];
                    var connectionInfoRow = result.Tables[1].Rows[0];

                    DataSource source = convertDataRowToDataSource(dataSourceRow);
                    source.DataConnectionInfo = convertDataRowToDataConnectionInfo(connectionInfoRow);

                    if (loadTables)
                    {
                        source.Tables.AddRange(GetTableInfosByDataSourceId(dataSourceId, loadColumns));
                    }

                    return source;
                }
            }
            catch (Exception ex)
            {
                throw new BasicDataException(string.Format("An unexpected error occurred while getting connections. ERROR: {0}", ex.Message), ex);
            }
        }

        public List<DataSource> GetAllByProjectId(int projectId, bool loadTables, bool loadColumns)
        {
            try
            {
                List<DataSource> resultSet;

                var db = SqlDatabaseFactory.Create(Constants.ConnectionStrings.Core);
                var command = db.GetStoredProcCommand("[dbo].[pr_GetDataSourcesByProjectId]");

                db.AddInParameter(command, "@project_id", SqlDbType.Int, projectId);

                using (var result = db.ExecuteDataSet(command))
                {
                    var defaultTable = result.Tables[0];
                    resultSet = new List<DataSource>(defaultTable.Rows.Count);
                    resultSet.AddRange(from DataRow row in defaultTable.Rows
                                       select convertDataRowToDataSource(row));
                }

                return resultSet;
            }
            catch (Exception ex)
            {
                throw new BasicDataException(string.Format("An unexpected error occurred while getting connections. ERROR: {0}", ex.Message), ex);
            }
        }

        #endregion

        #region Connection Info

        public List<DataConnectionInfo> GetAllConnections()
        {
            try
            {
                List<DataConnectionInfo> resultSet;

                var db = SqlDatabaseFactory.Create(Constants.ConnectionStrings.Core);
                var command = db.GetStoredProcCommand("[dbo].[pr_GetAllDataConnections]");

                using (var result = db.ExecuteDataSet(command))
                {
                    var defaultTable = result.Tables[0];
                    resultSet = new List<DataConnectionInfo>(defaultTable.Rows.Count);
                    resultSet.AddRange(from DataRow row in defaultTable.Rows
                                       select convertDataRowToDataConnectionInfo(row));
                }

                return resultSet;
            }
            catch (Exception ex)
            {
                throw new BasicDataException(string.Format("An unexpected error occurred while getting connections. ERROR: {0}", ex.Message), ex);
            }
        }

        public DataConnectionInfo CreateConnection(string serverName, string databaseName, string username, string password, DataConnectionType type)
        {
            try
            {
                var db = SqlDatabaseFactory.Create(Constants.ConnectionStrings.Core);

                var command = db.GetStoredProcCommand("[dbo].[pr_CreateDataConnectionInfo]");
                db.AddInParameter(command, "@server_name", SqlDbType.NVarChar, serverName);
                db.AddInParameter(command, "@database_name", SqlDbType.NVarChar, databaseName);
                db.AddInParameter(command, "@username", SqlDbType.NVarChar, username);
                db.AddInParameter(command, "@password", SqlDbType.NVarChar, password);
                db.AddInParameter(command, "@type", SqlDbType.SmallInt, type);

                using (DataSet dataSet = db.ExecuteDataSet(command))
                {
                    var connectionInfo = new DataConnectionInfo();

                    connectionInfo.DatabaseName = databaseName;
                    connectionInfo.ServerName = serverName;
                    connectionInfo.Username = username;
                    connectionInfo.Password = password;
                    connectionInfo.ConnectionType = type;
                    connectionInfo.DataConnectionInfoId = dataSet.Tables[0].Rows[0].Field<long>(0);
                    connectionInfo.ModifiedOn = DateTime.Now;
                    connectionInfo.CreatedOn = DateTime.Now;

                    return connectionInfo;
                }
            }
            catch (Exception ex)
            {
                throw new BasicDataException(string.Format("An unexpected error occurred while creating a connection. ERROR: {0}", ex.Message), ex);
            }
        }

        #endregion

        #region Database Tables/Columns

        public void UpdateTableInfoMappings(long dataSourceId, List<TableInfo> tableInfos)
        {
            var datasource = GetSingleById(dataSourceId, false, false);
            
            try
            {
                var db = SqlDatabaseFactory.Create(Constants.ConnectionStrings.Core);

                // Delete existing
                deleteAllTableInfosAndColumnInfos(db, dataSourceId);

                // Connect to the database in the connection info
                var databaseConn = DatabaseFacadeFactory.Create(datasource.DataConnectionInfo);

                // Add schema
                foreach (TableInfo tableInfo in tableInfos)
                {
                    // add table
                    long tableId = createTableInfo(db, dataSourceId, tableInfo);
                    // add columns
                    bulkInsertColumnInfos(db, tableId, databaseConn.GetColumns(tableInfo.ObjectId));
                }
            }
            catch (Exception ex)
            {
                throw new BasicDataException(string.Format("An unexpected error occurred while updating tables mappings. ERROR: {0}", ex.Message), ex);
            }
        }

        public List<TableInfo> GetTableInfosByDataSourceId(long dataSourceId, bool loadColumnInfos)
        {
            try
            {
                List<TableInfo> resultSet;

                var db = SqlDatabaseFactory.Create(Constants.ConnectionStrings.Core);
                var command = db.GetStoredProcCommand("[dbo].[pr_GetTableInfosByDataSourceId]");

                db.AddInParameter(command, "@data_source_id", DbType.Int64, dataSourceId);


                using (var result = db.ExecuteDataSet(command))
                {
                    var defaultTable = result.Tables[0];
                    resultSet = new List<TableInfo>(defaultTable.Rows.Count);

                    foreach (DataRow row in defaultTable.Rows)
                    {
                        TableInfo table = convertDataRowToTableInfo(row);

                        if (loadColumnInfos)
                        {
                            table.Columns.AddRange(GetColumnInfosByTableInfoId(table.Id));
                        }

                        resultSet.Add(table);
                    }
                }

                return resultSet;
            }
            catch (Exception ex)
            {
                throw new BasicDataException(string.Format("An unexpected error occurred while loading columns by table id. ERROR: {0}", ex.Message), ex);
            }
        }

        public List<ColumnInfo> GetColumnInfosByTableInfoId(long tableInfoId)
        {
            try
            {
                List<ColumnInfo> resultSet;

                var db = SqlDatabaseFactory.Create(Constants.ConnectionStrings.Core);
                var command = db.GetStoredProcCommand("[dbo].[pr_GetColumnsInfosByTableInfoId]");

                db.AddInParameter(command, "@table_info_id", DbType.Int64, tableInfoId);

                using (var result = db.ExecuteDataSet(command))
                {
                    var defaultTable = result.Tables[0];
                    resultSet = new List<ColumnInfo>(defaultTable.Rows.Count);
                    resultSet.AddRange(from DataRow row in defaultTable.Rows
                                       select convertDataRowToColumnInfo(row));
                }

                return resultSet;
            }
            catch (Exception ex)
            {
                throw new BasicDataException(string.Format("An unexpected error occurred while loading columns by table id. ERROR: {0}", ex.Message), ex);
            }
        }

        #endregion

        #region Private Methods

        #region Sql

        private void deleteAllTableInfosAndColumnInfos(Database database, long dataSourceId)
        {
            const string sproc = "[dbo].[pr_DeleteAllTablesAndColumns]";

            try
            {
                var command = database.GetStoredProcCommand(sproc);
                database.AddInParameter(command, "@data_source_id", DbType.Int64, dataSourceId);

                database.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw new BasicDataException(string.Format("An unexpected error occurred while deleting the tables and columns. ERROR: {0}", ex.Message), ex);
            }
        }

        private long createTableInfo(Database database, long dataSourceId, TableInfo table)
        {
            const string sproc = "[dbo].[pr_CreateTableInfo]";

            try
            {
                var command = database.GetStoredProcCommand(sproc);
                database.AddInParameter(command, "@data_source_id", DbType.Int64, dataSourceId);
                database.AddInParameter(command, "@table_type_id", DbType.Byte, table.TableType);
                database.AddInParameter(command, "@name", DbType.String, table.Name);
                database.AddInParameter(command, "@object_id", DbType.Int32, table.ObjectId);

                using (DataSet ds = database.ExecuteDataSet(command))
                {
                    return ds.Tables[0].Rows[0].Field<long>(0);
                }
            }
            catch (Exception ex)
            {
                throw new BasicDataException(string.Format("An unexpected error occurred while deleting the tables and columns. ERROR: {0}", ex.Message), ex);
            }
        }

        private void bulkInsertColumnInfos(SqlDatabase database, long tableId, List<ColumnInfo> columns)
        {
            if (!columns.Any())
            {
                return;
            }

            const string sproc = "[dbo].[pr_BulkAddColumnInfo]";

            try
            {
                var table = new DataTable();
                using (var reader = ObjectReader.Create(columns))
                {
                    table.Load(reader);
                }

                table.Columns.Remove("DataType");
                table.Columns.Remove("SupportedType");

                var command = database.GetStoredProcCommand(sproc);
                database.AddInParameter(command, "@table_info_id", SqlDbType.BigInt, tableId);
                database.AddInParameter(command, "@table_column_info", SqlDbType.Structured, table);
                database.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw new BasicDataException(string.Format("An unexpected error occurred while deleting the tables and columns. ERROR: {0}", ex.Message), ex);
            }
        }

        #endregion

        #region Conversions

        private DataConnectionInfo convertDataRowToDataConnectionInfo(DataRow row)
        {
            var connection = new DataConnectionInfo();

            connection.DataConnectionInfoId = row.Field<int>("data_connection_info_id");
            connection.ConnectionType = (DataConnectionType)row.Field<short>("data_connection_info_type_id");
            connection.ServerName = row.Field<string>("server_name");
            connection.DatabaseName = row.Field<string>("database_name");
            connection.Username = row.Field<string>("user_name");
            connection.Password = row.Field<string>("password");

            return connection;
        }
        
        private static DataSource convertDataRowToDataSource(DataRow row)
        {
            var dataSource = new DataSource();

            dataSource.Id = row.Field<long>("data_source_id");
            dataSource.Name = row.Field<string>("name");
            dataSource.Description = row.Field<string>("description");
            dataSource.ModifiedOn = row.Field<DateTime>("modified_datetime");
            dataSource.CreatedOn = row.Field<DateTime>("created_datetime");
            dataSource.DataConnectionInfoId = row.Field<int>("data_connection_info_id");

            return dataSource;
        }

        private ColumnInfo convertDataRowToColumnInfo(DataRow row)
        {
            var info = new ColumnInfo();

            info.Id = row.Field<long>("column_info_id");
            info.Name = row.Field<string>("name");
            info.IsComputed = row.Field<bool>("is_computed");
            info.IsIdentity = row.Field<bool>("is_identity");
            info.IsNullable = row.Field<bool>("is_nullable");
            info.MaxLength = row.Field<short>("max_length");
            info.Precision = row.Field<byte>("precision");
            info.ColumnId = row.Field<int>("db_column_id");
            info.DataType = (SqlColumnType)row.Field<short>("column_data_type_id");
            info.TableInfoId = row.Field<long>("table_info_id");

            return info;
        }

        private TableInfo convertDataRowToTableInfo(DataRow row)
        {
            var info = new TableInfo();

            info.Id = row.Field<long>("table_info_id");
            info.Name = row.Field<string>("name");
            info.TableType = (TableType)row.Field<byte>("table_type_id");
            info.ObjectId = row.Field<int>("object_id");
            info.CreatedOn = row.Field<DateTime>("created_datetime");
            info.ModifiedOn = row.Field<DateTime>("modified_datetime");
            info.DataSourceId = row.Field<long>("data_source_id");

            return info;
        }

        #endregion

        #endregion
    }
}