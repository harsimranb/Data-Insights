using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysis.DomainObjects.Exceptions
{
    public class BasicDataException : BasicException
    {
        public BasicDataException()
        {

        }

        public BasicDataException(string message, string additionalInfo = "")
            : base(message)
        {
            AdditionalInformation = additionalInfo;
        }

        public BasicDataException(string message, Exception innerException, string additionalInfo = "")
            : base(message, innerException)
        {
            AdditionalInformation = additionalInfo;
        }
    }
}
