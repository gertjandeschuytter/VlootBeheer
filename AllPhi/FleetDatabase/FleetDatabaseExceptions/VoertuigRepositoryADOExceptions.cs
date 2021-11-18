using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FleetDatabase.FleetDatabaseExceptions {
    public class VoertuigRepositoryADOExceptions : Exception {
        public VoertuigRepositoryADOExceptions() {
        }

        public VoertuigRepositoryADOExceptions(string message) : base(message) {
        }

        public VoertuigRepositoryADOExceptions(string message, Exception innerException) : base(message, innerException) {
        }

        protected VoertuigRepositoryADOExceptions(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
