using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAnalysis.DomainObjects.Common;

namespace DataAnalysis.DomainObjects.DataSource
{
    /// <summary>
    /// More Info:
    /// https://msdn.microsoft.com/en-us/library/ms187406.aspx
    /// https://msdn.microsoft.com/en-us/library/ms190334.aspx
    /// Representation of a table that has been imported from an external database into our system to query from.
    /// </summary>
    public class TableInfo
    {
        public readonly List<ColumnInfo> Columns = new List<ColumnInfo>();
        
        public long Id { get; set; }

        public int ObjectId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public TableType TableType { get; set; }

        public string Name { get; set; }

        public long DataSourceId { get; set; }
    }
}
