using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests.AdminRequests.HobbyRequests;

public class CreateHobbyRequest
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
} 