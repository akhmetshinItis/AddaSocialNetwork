namespace Web.Models.SignUp
{
    public class RegisterUserViewModel
    {
        public string? Email { get; set; } = default!;
        public string? Password { get; set; } = default!;
        public string? LastName { get; set; } = default!;
        public string? FirstName { get; set; } = default!;
        public string? Gender { get; set; } = default!;
        public string? Age { get; set; } = default!;
        public string? Country { get; set; } = default!;
    }
}