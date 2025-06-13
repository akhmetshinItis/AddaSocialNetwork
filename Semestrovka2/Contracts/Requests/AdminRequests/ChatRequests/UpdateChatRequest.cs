using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests.AdminRequests.ChatRequests;

public class UpdateChatRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid User1Id { get; set; }
    [Required]
    public Guid User2Id { get; set; }
} 