using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAnalysis.DomainObjects.DataSource;

namespace DataAnalysis.Web.Admin.ViewModels
{
    public class SelectableTableInfo
    {
        public TableInfo TableInfo { get; set; }
        public bool IsSelected { get; set; }
    }
}
