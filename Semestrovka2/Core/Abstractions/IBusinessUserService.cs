using Core.Entities;

namespace Core.Abstractions
{
    public interface IBusinessUserService
    {
        IQueryable<User> SearchUsers(string? searchString);
    }
}