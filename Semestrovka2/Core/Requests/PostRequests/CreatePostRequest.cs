using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.Requests.PostRequests
{
    public class CreatePostRequest
    {
        public string? Content { get; set; }
        public IFormFile? Photo { get; set; }
        
        [Required(ErrorMessage = "Пожалуйста, укажите ID пользователя")]
        public Guid UserId { get; set; }
    }
} 