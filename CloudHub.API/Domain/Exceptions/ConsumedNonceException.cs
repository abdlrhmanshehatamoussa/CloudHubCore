namespace CloudHub.Domain.Exceptions
{
    public class ConsumedNonceException : Exception
    {
        public ConsumedNonceException()
        {
        }

        public ConsumedNonceException(string? message) : base(message)
        {
        }
    }
}
