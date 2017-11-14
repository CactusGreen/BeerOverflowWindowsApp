using System;

namespace WebApi.Exceptions
{
    [Serializable]
    public class ArgumentsForProvidersException : Exception
    {
        public string InvalidArguments { get; }

        public ArgumentsForProvidersException() : base() { }
        public ArgumentsForProvidersException(string message) : base(message) { }
        public ArgumentsForProvidersException(string message, Exception inner) : base(message, inner) { }

        public ArgumentsForProvidersException(string message, string invalidArguments) :
            base(message)
        {
            InvalidArguments = invalidArguments;
        }

        protected ArgumentsForProvidersException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }
}
