using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Exceptions
{
    public class TankKaartException : Exception
    {
        public TankKaartException(string message) : base(message)
        {
        }

        public TankKaartException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
