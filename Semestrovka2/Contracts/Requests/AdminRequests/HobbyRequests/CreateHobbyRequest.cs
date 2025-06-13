using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Contracts.Requests.AdminRequests.HobbyRequests;

public class CreateHobbyRequest
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Display(Name = "Фото хобби")]
    public List<IFormFile>? Photos { get; set; }
} 