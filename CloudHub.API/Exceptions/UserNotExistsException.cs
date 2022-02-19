namespace CloudHub.Domain.Exceptions
{
    public class UserNotExistsException : Exception
    {
        public UserNotExistsException()
        {
        }

        public UserNotExistsException(string? message) : base(message)
        {
        }
    }
}
