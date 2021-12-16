using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Exceptions
{
    class TankkaartRepositoryADOException : Exception
    {
        public TankkaartRepositoryADOException(string message) : base(message)
        {
        }

        public TankkaartRepositoryADOException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
