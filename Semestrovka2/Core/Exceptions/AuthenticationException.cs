namespace Core.Exceptions ;

    /// <summary>
    /// Исключение об ошибке аутентификации
    /// </summary>
    public class AuthenticationException : ApplicationExceptionBase
    {
        public AuthenticationException(string message) : base(message)
        {
        }
    }