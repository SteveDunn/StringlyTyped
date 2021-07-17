using System;
using System.Runtime.Serialization;

namespace StringlyTyped
{
    [Serializable]
    public class ValueObjectSetupException : Exception
    {
        public ValueObjectSetupException()
        {
        }

        public ValueObjectSetupException(string message) : base(message)
        {
        }

        public ValueObjectSetupException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ValueObjectSetupException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}