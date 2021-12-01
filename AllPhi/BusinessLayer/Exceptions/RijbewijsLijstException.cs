using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Exceptions
{
    class RijbewijsLijstException : Exception
    {
        public RijbewijsLijstException(string message) : base(message)
        {
        }

        public RijbewijsLijstException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
