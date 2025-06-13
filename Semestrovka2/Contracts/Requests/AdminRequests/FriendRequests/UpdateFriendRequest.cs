using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests.AdminRequests.FriendRequests
{
    public class UpdateFriendRequest
    {
        public Guid Id { get; set; }

        public Guid User1 { get; set; }

        public Guid User2 { get; set; }
    }
} 