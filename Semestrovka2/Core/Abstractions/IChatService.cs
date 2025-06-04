using Core.Entities;

namespace Core.Abstractions
{
    public interface IChatService
    {
        public IQueryable<Chat> GetChats();
    }
}