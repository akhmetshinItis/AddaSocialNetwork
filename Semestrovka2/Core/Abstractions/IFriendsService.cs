using Core.Entities;
using MimeKit.Encodings;

namespace Core.Abstractions
{
    public interface IFriendsService
    {
        public Task<int> AddFriendAsync(Guid friendId);
        public Task RemoveFriendAsync(Guid friendId);
        public Task<bool> IsFriendAsync(Guid friendId);
        public IQueryable<User> GetFriends(Guid userId);
    }
}