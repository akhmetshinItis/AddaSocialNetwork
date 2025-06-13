namespace Contracts.Requests.AdminRequests.MessageRequests
{
    public class GetAllMessagesResponseMessageItem
    {
        public Guid Id { get; set; }
        public Guid? ChatId { get; set; }
        public Guid? SenderId { get; set; }
        public string SenderName { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
} 