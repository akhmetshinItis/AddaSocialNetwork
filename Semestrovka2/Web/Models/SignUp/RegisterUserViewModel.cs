using System.ComponentModel.DataAnnotations;

namespace Web.Models.SignUp
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        [StringLength(100, ErrorMessage = "Пароль должен быть не менее {2} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]).+$", 
            ErrorMessage = "Пароль должен содержать хотя бы одну заглавную букву, одну цифру и один спецсимвол.")]
        public string? Password { get; set; }
        
        [Required(ErrorMessage = "Имя обязательно")]
        [Display(Name = "Имя")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна")]
        [Display(Name = "Фамилия")]
        public string? LastName { get; set; }

        [Range(0, 150, ErrorMessage = "Возраст должен быть от 0 до 150")]
        [Display(Name = "Возраст")]
        public int Age { get; set; }

        [Display(Name = "Страна")]
        public string? Country { get; set; }
        
        public bool Gender { get; set; }
    }
}