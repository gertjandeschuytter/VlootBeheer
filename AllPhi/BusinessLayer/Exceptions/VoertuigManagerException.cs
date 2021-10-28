using System;
using System.Runtime.Serialization;

namespace BusinessLayer.Managers {
    [Serializable]
    internal class VoertuigManagerException : Exception {
        public VoertuigManagerException() {
        }

        public VoertuigManagerException(string message) : base(message) {
        }

        public VoertuigManagerException(string message, Exception innerException) : base(message, innerException) {
        }

        protected VoertuigManagerException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}