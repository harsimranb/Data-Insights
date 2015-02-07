using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace DataAnalysis.Infrastructure.Framework
{
    internal static class SqlDatabaseFactory
    {
        internal static SqlDatabase Create(string connectionName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionName];
            Debug.Assert(connectionString != null);
            return new SqlDatabase(connectionString.ToString());
        }
    }
}
