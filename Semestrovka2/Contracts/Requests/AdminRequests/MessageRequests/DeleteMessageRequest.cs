using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests.AdminRequests.MessageRequests;

public class DeleteMessageRequest
{
    [Required]
    public Guid Id { get; set; }
} 