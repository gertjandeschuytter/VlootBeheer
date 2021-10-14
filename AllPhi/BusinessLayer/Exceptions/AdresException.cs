using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Exceptions
{
    class AdresException : Exception
    {
        public AdresException(string message) : base(message)
        {
        }

        public AdresException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
