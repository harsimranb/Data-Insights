using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysis.DomainObjects.Exceptions
{
    public class BasicException : Exception
    {
        public BasicException()
        {

        }

        public BasicException(string message, string additionalInfo = "")
            : base(message)
        {
            AdditionalInformation = additionalInfo;
        }

        public BasicException(string message, Exception innerException, string additionalInfo = "")
            : base(message, innerException)
        {
            AdditionalInformation = additionalInfo;
        }

        public string AdditionalInformation { get; set; }
    }
}
