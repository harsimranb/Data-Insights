using System;
using System.Linq;
using DataAnalysis.DomainObjects.Attributes;
using DataAnalysis.DomainObjects.DataSource;
using DataAnalysis.Infrastructure.Extensions;

namespace DataAnalysis.Infrastructure.Utils
{
    public static class SqlColumnUtil
    {
        /// <summary>
        /// Convert from Sql Server data_type field (from schema queries) to our SqlColumnType.
        /// </summary>
        /// <param name="dataTypeName">data_type from sys.tables query.</param>
        /// <returns></returns>
        public static SqlColumnType? ConvertDataTypeNameToColumnType(string dataTypeName)
        {
            foreach (Enum enumValue in Enum.GetValues(typeof(SqlColumnType)))
            {
                var attribute = enumValue.GetAttribute<SqlColumnTypeInfoAttribute>();
                if (attribute == null)
                {
                    continue;
                }

                if (attribute.Name.Contains(dataTypeName))
                {
                    return (SqlColumnType)enumValue;
                }
            }

            return null;
        }
    }
}
