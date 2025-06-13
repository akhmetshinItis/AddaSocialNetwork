using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.Requests.PhotoRequests
{
    public class CreatePhotoRequest
    {
        [Required(ErrorMessage = "Пожалуйста, выберите фото")]
        public IFormFile? Photo { get; set; }
        
        [Required(ErrorMessage = "Пожалуйста, укажите ID пользователя")]
        public Guid UserId { get; set; }
        
        public Guid? AlbumId { get; set; }
    }
} 