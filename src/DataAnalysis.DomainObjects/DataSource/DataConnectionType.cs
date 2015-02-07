using System.ComponentModel.DataAnnotations;

namespace DataAnalysis.DomainObjects.DataSource
{
    public enum DataConnectionType : short
    {
        [Display(Name = "Microsoft Sql Server")]
        MsSqlServer = 1
    }
}
