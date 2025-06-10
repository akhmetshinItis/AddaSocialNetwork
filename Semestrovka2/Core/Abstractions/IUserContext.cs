namespace Core.Abstractions ;

    public interface IUserContext
    {
        public string? GetUserEmail();
        
        public Guid GetUserId();
    }