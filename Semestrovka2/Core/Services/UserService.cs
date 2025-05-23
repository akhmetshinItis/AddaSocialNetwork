using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Core.Abstractions;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Services
{
     /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public Task<IdentityResult> RegisterUserAsync(User user, string password)
            => _userManager.CreateAsync(user, password);
        
        public async Task<User?> FindUserByEmailAsync(string email)
            => await _userManager.FindByEmailAsync(email);

        public Task<IdentityResult> AddRoleAsync(User user, string role)
            => _userManager.AddToRoleAsync(user, role);

        public Task<IdentityResult> AddClaimsAsync(User user, IEnumerable<Claim> claims)
            => _userManager.AddClaimsAsync(user, claims);

        public Task<bool> CheckPasswordAsync(User user, string password)
            => _userManager.CheckPasswordAsync(user, password);

        public Task<IList<string>> GetRolesAsync(User user)
            => _userManager.GetRolesAsync(user);
        
        public async Task<string?> GetRoleAsync(User user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            return claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        }

        public Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
            => _userManager.ResetPasswordAsync(user, token, newPassword);
        
        public Task<string> GeneratePasswordResetTokenAsync(User user)
            => _userManager.GeneratePasswordResetTokenAsync(user);

        public Task<User?> GetUserById(Guid id)
            => _userManager.FindByIdAsync(id.ToString());

         public async Task<SignInResult> LoginAsync(string login, string password)
         {
             var user = await _userManager.FindByEmailAsync(login);
             if (user == null)
                 return SignInResult.Failed;
            return await _signInManager.PasswordSignInAsync(user, password, false, false);
         }
    }
}