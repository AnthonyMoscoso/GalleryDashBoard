namespace DashboardGallery.ViewModels
{
    public class CheckeableItemTableModelView
    {
        public string Source { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public bool IsChecked { get; set; }
    }
}
