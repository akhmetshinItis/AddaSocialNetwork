using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Requests.ProfileRequests.UpdateProfile
{
    public class UpdateProfileImageRequest : IRequest
    {
        public IFormFile? ProfileImage { get; set; }
        public IFormFile? BackgroundImage { get; set; }
    }
} 