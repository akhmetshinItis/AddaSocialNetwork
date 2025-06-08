namespace Web.Models.ProfilePhotos
{
    public class ProfilePhotoViewModel
    {
        public ProfilePhotoViewModel(string profileImage, string backgroundImage)
        {
            ProfileImage = profileImage;
            BackgroundImage = backgroundImage;
        }

        public string ProfileImage { get; set; }
        public string BackgroundImage { get; set; }
    }
}