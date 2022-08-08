using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseTechnology.Common.Tools.Exceptions
{
    public class BadRequestException:Exception
    {
        public BadRequestException(string message) : base(message==null ? "Bad Request":message)
        {
        }
    }
}
