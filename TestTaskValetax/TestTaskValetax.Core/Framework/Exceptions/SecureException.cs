namespace TestTaskValetax.Core.Framework.Exceptions;

[Serializable]
public class SecureException : Exception
{
    public SecureException(string? message = null) : base(message)
    { }

    public SecureException(Exception? innerException, string? message = null) : base (message, innerException)
    {}
}
