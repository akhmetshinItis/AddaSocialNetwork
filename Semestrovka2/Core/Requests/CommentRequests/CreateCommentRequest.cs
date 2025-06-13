using System.ComponentModel.DataAnnotations;

namespace Core.Requests.CommentRequests
{
    public class CreateCommentRequest
    {
        public string? Content { get; set; }
        
        [Required(ErrorMessage = "Пожалуйста, укажите ID пользователя")]
        public Guid UserId { get; set; }
        
        [Required(ErrorMessage = "Пожалуйста, укажите ID поста")]
        public Guid PostId { get; set; }
    }
} 