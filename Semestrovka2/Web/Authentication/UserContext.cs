using System.Security.Claims;
using Core.Abstractions;

namespace Web.Authentication ;

    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public string? GetUserEmail()
        {
            return _httpContextAccessor.HttpContext?
                .User?
                .Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }

        public Guid? GetUserId()
        {
            var id = _httpContextAccessor.HttpContext?
                .User?
                .Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.Authentication)?.Value;
            return Guid.Parse(id ?? throw new NullReferenceException("User is not authenticated."));
        }
    }