using System.Security.Claims;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Abstractions
{

    /// <summary>
    /// Сервис по работе с пользователем
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Зарегистрировать пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        public Task<IdentityResult> RegisterUserAsync(IdentityUser<Guid> user, string password);

        /// <summary>
        /// Найти пользователя по Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<IdentityUser<Guid>?> FindUserByEmailAsync(string email);

        /// <summary>
        /// Добавить роль пользователю
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="role">Роль</param>
        /// <returns></returns>
        public Task<IdentityResult> AddRoleAsync(IdentityUser<Guid> user, string role);

        /// <summary>
        /// Добавить клеймы
        /// </summary>
        /// <param name="user">пользователь</param>
        /// <param name="claims">Клеймы</param>
        /// <returns></returns>
        public Task<IdentityResult> AddClaimsAsync(IdentityUser<Guid> user, IEnumerable<Claim> claims);

        /// <summary>
        /// Проверить пароль
        /// </summary>
        /// <param name="user">пользователь</param>
        /// <param name="password">пароль</param>
        /// <returns></returns>
        public Task<bool> CheckPasswordAsync(IdentityUser<Guid> user, string password);

        /// <summary>
        /// Получить роли пользователя
        /// </summary>
        /// <param name="user">пользователь</param>
        /// <returns></returns>
        public Task<IList<string>> GetRolesAsync(IdentityUser<Guid> user);

        /// <summary>
        /// Получить роль пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string?> GetRoleAsync(IdentityUser<Guid> user);

        /// <summary>
        /// Сменить пароль пользователю
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="token">Токен смены пароля</param>
        /// <param name="newPassword">Новый пароль</param>
        /// <returns></returns>
        public Task<IdentityResult> ResetPasswordAsync(IdentityUser<Guid> user, string token, string newPassword);

        /// <summary>
        /// Сгенерировать токен смены пароля
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        public Task<string> GeneratePasswordResetTokenAsync(IdentityUser<Guid> user);

        /// <summary>
        /// Получить пользователя по идентификатору
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <returns>-</returns>
        public Task<IdentityUser<Guid>?> GetUserById(Guid id);
        
        /// <summary>
        /// Аутентифицировать пользователя
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        public Task<SignInResult> LoginAsync(string login, string password);
    }
}