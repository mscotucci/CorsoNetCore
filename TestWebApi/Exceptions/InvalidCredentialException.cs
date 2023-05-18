namespace TestWebApi.Exceptions
{
    public class InvalidCredentialException : Exception
    {
        public InvalidCredentialException(string? message=null):base(message ??  "Credenziali non valide")
        {
            
        }
    }
}
