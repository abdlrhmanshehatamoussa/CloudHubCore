using System.Runtime.Serialization;

namespace CloudHub.Domain.Exceptions
{
 
    public class InvalidCollectionException : Exception
    {
        public InvalidCollectionException()
        {
        }

        public InvalidCollectionException(string? message) : base(message)
        {
        }

        public InvalidCollectionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidCollectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}