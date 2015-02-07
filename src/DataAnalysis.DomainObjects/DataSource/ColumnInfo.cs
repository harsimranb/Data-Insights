using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace DataAnalysis.DomainObjects.DataSource
{
    /// <summary>
    /// More Info: https://msdn.microsoft.com/en-us/library/ms176106.aspx
    /// This object is a representation of a external column (from data source -> table info) that has been imported into our database.
    /// </summary>
    public class ColumnInfo
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public SqlColumnType? DataType { get; set; }

        public short? DataTypeId
        {
            get
            {
                if (DataType == null)
                {
                    return null;
                }

                return (short) DataType;
            }
        }

        public bool IsComputed { get; set; }

        public bool IsIdentity { get; set; }

        public bool IsNullable { get; set; }

        public short MaxLength { get; set; }

        public byte Precision { get; set; }

        public int ColumnId { get; set; }

        public bool SupportedType { get; set; }

        public long? TableInfoId { get; set; }
    }
}
