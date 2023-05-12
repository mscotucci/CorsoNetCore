namespace TestWebApi.Exceptions
{
    public class AuthorNotFoundException : Exception
    {
        public AuthorNotFoundException(string? message) : base(message ?? "author not found")
        {
        }
    }
}
