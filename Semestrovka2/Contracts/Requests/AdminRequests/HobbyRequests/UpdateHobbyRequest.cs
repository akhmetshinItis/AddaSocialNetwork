using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests.AdminRequests.HobbyRequests;

public class UpdateHobbyRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
} 