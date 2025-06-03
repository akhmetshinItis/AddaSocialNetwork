namespace Contracts.Requests.ChatRequests.GetAllChats
{
    public class GetAllChatsResponseItem
    {
        public Guid User1Id { get; set; }
        public Guid User2Id { get; set; }
        public GetAllChatsMessageResponseItem LastMessage { get; set; } = new();
    }
}