using Bsn.Utilities.PlaceHolder;

namespace DashboardGallery.Shared.Messages.Model
{
    public class MessageBoxConfig
    {
        public string BorderColor { get; set; } = "#FFDE59";
        public string ImageUrl { get; set; } = PlaceholderUrls.Size150;
        public string TitleColor { get; set; } = "#FFDE59";
        public string ButtonColor { get; set; } = "#FFDE59";
        public string ButtonTextColor { get; set; } = "black";
        public int ButtonBoder { get; set; } = 0;
        public string ButtonBoderColor { get; set; } = "none";
        public string BackgroundColor { get; set; } = "#FFFFFF";
        public string TextColor { get; set; } = "black";
    }
}
