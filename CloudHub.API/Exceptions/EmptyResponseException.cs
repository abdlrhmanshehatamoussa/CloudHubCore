namespace CloudHub.API.Exceptions
{
    public class EmptyResponseException : Exception
    {
        public EmptyResponseException()
        {
        }

        public EmptyResponseException(string? message) : base(message)
        {
        }
    }
}