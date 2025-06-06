namespace Contracts.Requests.ProfileRequests
{
    public class GetProfilePhotosResponse
    {
        public string ProfileImage { get; set; } = string.Empty;
        public string BackgroundImage { get; set; } = string.Empty;
    }
}