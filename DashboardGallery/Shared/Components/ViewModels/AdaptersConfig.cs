namespace DashboardGallery.ViewModels
{
    public class AdaptersConfig
    {
        public AdaptersConfig(bool hiddenAdd,bool hiddenRemove,bool hiddenConfirmed,bool disabled) 
        { 
            HiddenAdd = hiddenAdd;
            HiddenRemove = hiddenRemove;
            HiddenConfirmed = hiddenConfirmed;
            Disabled = disabled;
        }
        public bool HiddenAdd { get; set; }
        public bool HiddenRemove { get; set; }
        public bool HiddenConfirmed { get; set; }
        public bool Disabled { get; set; }
    }
}
