using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAnalysis.DomainObjects.Common;

namespace DataAnalysis.DomainObjects.DataSource
{
    /// <summary>
    /// Representation of a data source created and the tables added.
    /// </summary>
    public class DataSource
    {
        public readonly List<TableInfo> Tables = new List<TableInfo>();

        [Required]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int DataConnectionInfoId { get; set; }

        public DataConnectionInfo DataConnectionInfo { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime ModifiedOn { get; set; }

        [Required]
        public int ProjectId { get; set; }
    }
}
