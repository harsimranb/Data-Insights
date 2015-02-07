using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAnalysis.DomainObjects.DataSource;

namespace DataAnalysis.Interfaces.Data
{
    public abstract class DatabaseFacade
    {
        protected readonly string ServerName;
        protected readonly string DatabaseName;
        protected string Username;
        protected readonly string Password;

        protected DatabaseFacade(DataConnectionInfo info)
        {
            ServerName = info.ServerName;
            DatabaseName = info.DatabaseName;
            Username = info.Username;
            Password = info.Password;
        }

        protected DatabaseFacade(
            string serverName,
            string databaseName,
            string userName,
            string password)
        {
            ServerName = serverName;
            DatabaseName = databaseName;
            Username = userName;
            Password = password;
        }

        public abstract string ConnectionString { get; }

        public abstract bool TestConnection();

        public abstract List<TableInfo> GetTables();

        public abstract List<ColumnInfo> GetColumns(int objectId);
    }
}
