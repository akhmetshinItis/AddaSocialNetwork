namespace Contracts.Requests.UserRequests.LoginUser ;

    /// <summary>
    /// Запрос на авторизацию
    /// </summary>
    public class LoginUserRequest
    {
        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }  = default!;
        
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = default!;
    }