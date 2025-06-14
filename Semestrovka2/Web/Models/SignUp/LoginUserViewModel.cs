namespace Web.Models.SignUp
{
    using System.ComponentModel.DataAnnotations;

    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        public string? Email { get; set; } = default!;

        [Required(ErrorMessage = "Пароль обязателен")]
        public string? Password { get; set; } = default!;
    }
}