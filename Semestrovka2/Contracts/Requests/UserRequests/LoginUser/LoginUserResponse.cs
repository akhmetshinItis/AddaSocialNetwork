using Microsoft.AspNetCore.Identity;

namespace Contracts.Requests.UserRequests.LoginUser ;

    /// <summary>
    /// Ответ на запрос <see cref="LoginUserRequest"/>
    /// </summary>
    public class LoginUserResponse(SignInResult result)
    {
        /// <summary>
        /// Токен
        /// </summary>
        public SignInResult Result { get;} = result;
    }