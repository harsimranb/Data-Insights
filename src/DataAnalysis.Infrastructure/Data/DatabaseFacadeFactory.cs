using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAnalysis.DomainObjects.DataSource;
using DataAnalysis.DomainObjects.Exceptions;
using DataAnalysis.Interfaces.Data;

namespace DataAnalysis.Infrastructure.Data
{
    public static class DatabaseFacadeFactory
    {
        public static DatabaseFacade Create(DataConnectionInfo info)
        {
            switch (info.ConnectionType)
            {
                case DataConnectionType.MsSqlServer:
                    return new SqlServerDatabaseFacade(info);
                default:
                    throw new BasicException("Database type is not yet supported.", string.Format("InfoId: {0} Connection Type: {1}", info.DataConnectionInfoId, info.ConnectionType));
            }
        }

        public static DatabaseFacade Create(
            string serverName,
            string databaseName,
            string userName,
            string password,
            DataConnectionType type)
        {
            switch (type)
            {
                case DataConnectionType.MsSqlServer:
                    return new SqlServerDatabaseFacade(serverName, databaseName, userName, password);
                default:
                    throw new BasicException("Database type is not yet supported.", string.Format("Connection Type: {0}", type));
            }
        }
    }
}
