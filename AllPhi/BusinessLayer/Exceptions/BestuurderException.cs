using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Exceptions
{
    class BestuurderException : Exception
    {
        public BestuurderException(string message) : base(message)
        {
        }

        public BestuurderException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
