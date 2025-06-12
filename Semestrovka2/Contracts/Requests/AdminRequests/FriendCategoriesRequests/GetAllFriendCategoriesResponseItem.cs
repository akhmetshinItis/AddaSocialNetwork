namespace Contracts.Requests.AdminRequests.FriendCategoriesRequests
{
    public class GetAllFriendCategoriesResponseItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }
        public Guid FriendCategoryId { get; set; }
        public string Name { get; set; }
    }
}