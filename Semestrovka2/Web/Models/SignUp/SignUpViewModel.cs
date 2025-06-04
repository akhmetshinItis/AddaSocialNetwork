namespace Web.Models.SignUp
{
    public class SignUpViewModel
    {
        public RegisterUserViewModel RegisterUserViewModel { get; set; } = new();
        public LoginUserViewModel LoginUserViewModel { get; set; } = new();
    }
}