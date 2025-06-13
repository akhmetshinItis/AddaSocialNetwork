using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests.AdminRequests.ChatRequests;

public class DeleteChatRequest
{
    [Required]
    public Guid Id { get; set; }
} 