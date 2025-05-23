using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests.UserRequests.RegisterUser
{
    /// <summary>
    /// Запрос на создание пользователя
    /// </summary>
    public class RegisterUserRequest
    {
        /// <summary>
        /// Почта
        /// </summary>
        [Required]
        public string Email { get; set; } = default!;

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        public string Password { get; set; } = default!;
    }
}