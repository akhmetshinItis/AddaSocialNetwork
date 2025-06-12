namespace Contracts.Requests.AdminRequests.ChatRequests
{
    public class GetAllChatsResponseChatItem
    {
        public Guid Id { get; set; }
        public Guid User1Id { get; set; }
        public string User1Name { get; set; } = default!;
        public Guid User2Id { get; set; }
        public string User2Name { get; set; } = default!;
        public int MessagesCount { get; set; }
        public string? LastMessageContent { get; set; }
        public DateTime? LastMessageTime { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
} 