namespace Contracts.Requests.ChatRequests.GetAllChats
{
    public class GetAllChatsMessageResponseItem
    {
        public Guid ChatId { get; set; }
        public Guid? SenderId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
    }
}