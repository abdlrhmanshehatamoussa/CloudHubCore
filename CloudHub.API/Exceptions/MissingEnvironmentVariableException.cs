namespace CloudHub.API
{
    public class MissingEnvironmentVariableException : Exception
    {
        public MissingEnvironmentVariableException(string? variable) : base($"Missing environment variable: {variable}")
        {
        }
    }
}
