using Bsn.Utilities.PlaceHolder;

namespace DashboardGallery.Shared.Messages.Model
{
    public class QuestionMessageBoxConfig
    {
        public string BorderColor { get; set; } = "#FFDE59";
        public string ImageUrl { get; set; } = PlaceholderUrls.Size150;
        public string TitleColor { get; set; } = "#FFDE59";
        public string FirstButtonColor { get; set; } = "#FFDE59";
        public string SecondButtonColor { get; set; } = "#FF0000";
        public string BackgroundColor { get; set; } = "#FFFFFF";

        public static QuestionMessageBoxConfig DeleteConfig => new()
        {
            SecondButtonColor = "black",
            FirstButtonColor = "#FF0000",
            BorderColor = "#FF0000",
            TitleColor = "#FF0000",
            ImageUrl = "https://media1.giphy.com/media/v1.Y2lkPTc5MGI3NjExZTcyOTQ3ZDQzZTRhYzAzYmRlZDdkOTIzNzljMTZlM2ZkOGQwMjZiYSZlcD12MV9pbnRlcm5hbF9naWZzX2dpZklkJmN0PXM/f9ZDFh7l8EdESPCVee/giphy.gif"

        };

        public static QuestionMessageBoxConfig UnsavesChagesConfig => new()
        {
            SecondButtonColor = "#154854",
            FirstButtonColor = "#FF0000",
            BorderColor = "#FF0000",
            TitleColor = "#FF0000",
            ImageUrl = "https://media1.giphy.com/media/v1.Y2lkPTc5MGI3NjExZTcyOTQ3ZDQzZTRhYzAzYmRlZDdkOTIzNzljMTZlM2ZkOGQwMjZiYSZlcD12MV9pbnRlcm5hbF9naWZzX2dpZklkJmN0PXM/f9ZDFh7l8EdESPCVee/giphy.gif"

        };
    }
}

