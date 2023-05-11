namespace TestWebApi.Exceptions;

public class BookNotFoundException : Exception
{
    public BookNotFoundException(string? message) : base(message ?? "book not found")
    {
    }
}
