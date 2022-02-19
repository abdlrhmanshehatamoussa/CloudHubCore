namespace CloudHub.Domain.Exceptions
{
    public class InvalidNonceException : Exception
    {
        public InvalidNonceException()
        {
        }

        public InvalidNonceException(string? message) : base(message)
        {
        }
    }
}
