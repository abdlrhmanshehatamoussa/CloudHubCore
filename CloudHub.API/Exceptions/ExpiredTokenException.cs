namespace CloudHub.Domain.Exceptions
{
    public class ExpiredTokenException : Exception
    {
        public ExpiredTokenException()
        {
        }

        public ExpiredTokenException(string? message) : base(message)
        {
        }
    }
}
