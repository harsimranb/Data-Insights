using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAnalysis.DomainObjects.DataSource;
using DataAnalysis.DomainObjects.Exceptions;
using DataAnalysis.Infrastructure.Utils;
using DataAnalysis.Interfaces.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace DataAnalysis.Infrastructure.Data
{
    public class SqlServerDatabaseFacade : DatabaseFacade
    {
        private string _connectionString;

        public SqlServerDatabaseFacade(DataConnectionInfo info)
            : base(info)
        {
        }

        public SqlServerDatabaseFacade(string serverName, string databaseName, string userName, string password)
            : base(serverName, databaseName, userName, password)
        {
        }

        public override string ConnectionString
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_connectionString))
                    return _connectionString;

                var conectionStringBuilder = new SqlConnectionStringBuilder();
                conectionStringBuilder.DataSource = ServerName;
                conectionStringBuilder.InitialCatalog = DatabaseName;
                conectionStringBuilder.UserID = Username;
                conectionStringBuilder.Password = Password;
                _connectionString = conectionStringBuilder.ToString();

                return _connectionString;
            }
        }

        public override bool TestConnection()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();

                    var state = (conn.State == ConnectionState.Open);
                    if (state)
                        conn.Close();

                    return state;
                }
                catch
                {
                    return false;
                }
            }
        }

        public override List<TableInfo> GetTables()
        {
            var resultSet = new List<TableInfo>();
            const string sql = @"SELECT
	                                name,
	                                object_id,
	                                create_date,
	                                modify_date,
	                                table_type=1
                                FROM sys.tables (NOLOCK)
                                WHERE type='U'
                                UNION
                                SELECT
	                                name,
	                                object_id,
	                                create_date,
	                                modify_date,
	                                table_type=2
                                FROM sys.views (NOLOCK)
                                WHERE type='V'";

            try
            {
                var db = new SqlDatabase(ConnectionString);
                using (var result = db.ExecuteDataSet(CommandType.Text, sql))
                {
                    var defaultTable = result.Tables[0];
                    foreach (DataRow row in defaultTable.Rows)
                    {
                        var tableInfo = new TableInfo();
                        tableInfo.CreatedOn = row.Field<DateTime>("create_date");
                        tableInfo.ObjectId = row.Field<int>("object_id");
                        tableInfo.Name = row.Field<string>("name");
                        tableInfo.ModifiedOn = row.Field<DateTime>("modify_date");
                        tableInfo.TableType = (TableType)row.Field<int>("table_type");
                        resultSet.Add(tableInfo);
                    }
                }

                return resultSet;
            }
            catch (Exception ex)
            {
                throw new BasicDataException(string.Format("An unexpected error occurred which getting database schema. ERROR: {0}", ex.Message), ex);
            }
        }

        public override List<ColumnInfo> GetColumns(int objectId)
        {
            var resultSet = new List<ColumnInfo>();
            const string sql = @"	SELECT
		                                col_name = Al.name,
		                                data_type_name = T.name,
		                                Al.is_identity,
		                                AL.is_computed,
		                                AL.is_nullable,
		                                AL.max_length,
		                                AL.precision,
		                                AL.column_id
	                                FROM sys.all_columns AL (NOLOCK)
	                                INNER JOIN sys.types T ON AL.user_type_id = T.user_type_id 
	                                WHERE object_id = @object_id";

            try
            {
                var db = new SqlDatabase(ConnectionString);
                
                var command = db.GetSqlStringCommand(sql);
                db.AddInParameter(command, "@object_id", SqlDbType.Int, objectId);

                using (var result = db.ExecuteDataSet(command))
                {
                    var defaultTable = result.Tables[0];
                    foreach (DataRow row in defaultTable.Rows)
                    {
                        var columnInfo = new ColumnInfo();
                        columnInfo.ColumnId = row.Field<int>("column_id");
                        columnInfo.Name = row.Field<string>("col_name");
                        columnInfo.IsComputed = row.Field<bool>("is_computed");
                        columnInfo.IsIdentity = row.Field<bool>("is_identity");
                        columnInfo.IsNullable = row.Field<bool>("is_nullable");
                        columnInfo.MaxLength = row.Field<short>("max_length");
                        columnInfo.Precision = row.Field<byte>("precision");
                        columnInfo.DataType = SqlColumnUtil.ConvertDataTypeNameToColumnType(row.Field<string>("data_type_name"));
                        columnInfo.SupportedType = columnInfo.DataType != null;
                        resultSet.Add(columnInfo);
                    }
                }

                return resultSet;
            }
            catch (Exception ex)
            {
                throw new BasicDataException(string.Format("An unexpected error occurred which getting database schema. ERROR: {0}", ex.Message), ex);
            }
        }
    }
}
