using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysis.DomainObjects.Attributes
{
    public class SqlColumnTypeInfoAttribute : Attribute
    {
        public SqlColumnTypeInfoAttribute(params string[] names)
        {
            Name = names;
        }

        public string[] Name { get; private set; }
    }
}
