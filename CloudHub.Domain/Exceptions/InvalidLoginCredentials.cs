namespace CloudHub.Domain.Exceptions
{

    public class InvalidLoginCredentials : Exception
    {
        public InvalidLoginCredentials()
        {
        }

        public InvalidLoginCredentials(string? message) : base(message)
        {
        }
    }
}
