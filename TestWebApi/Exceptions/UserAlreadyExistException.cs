namespace TestWebApi.Exceptions
{
    public class UserAlreadyExistException : Exception
    {
        public UserAlreadyExistException(string? message=null) : base(message ?? "Utente Esiste gia") { }
    }
}
