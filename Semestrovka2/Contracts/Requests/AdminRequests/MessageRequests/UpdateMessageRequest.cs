using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests.AdminRequests.MessageRequests;

public class UpdateMessageRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid ChatId { get; set; }
    [Required]
    public Guid SenderId { get; set; }
    [Required]
    public string Content { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public DateTime? Timestamp { get; set; }
} 