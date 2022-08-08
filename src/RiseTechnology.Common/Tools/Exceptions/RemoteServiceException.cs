using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RiseTechnology.Common.Tools.Exceptions
{
    public class RemoteServiceException : Exception
    {
        public RemoteServiceException():base("Remote Service Returned A Different Result Than 200 Code")
        {
        }
    }
}
