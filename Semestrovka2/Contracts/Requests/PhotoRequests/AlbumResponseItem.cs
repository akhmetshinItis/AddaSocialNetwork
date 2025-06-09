namespace Contracts.Requests.PhotoRequests
{
    public class AlbumResponseItem
    {
        public string Name { get; set; } = string.Empty;
        public List<PhotoResponseItem> Photos { get; set; } = new();
    }
}