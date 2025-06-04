using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

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

        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public bool Gender { get; set; }
        public int Age { get; set; }
        public string Country { get; set; } = default!;
    }
}