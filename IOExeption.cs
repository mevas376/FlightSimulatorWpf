using System;
using System.Runtime.Serialization;

namespace FlightSimulator
{
    [Serializable]
    internal class IOExeption : Exception
    {
        public IOExeption()
        {
        }

        public IOExeption(string message) : base(message)
        {
        }

        public IOExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IOExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}