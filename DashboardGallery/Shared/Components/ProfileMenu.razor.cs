using Bsn.Utilities.Constants;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Components
{
    public partial class ProfileMenu : ComponentBase
    {
        [Parameter]
        public string ImgSrc { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public string Class { get; set; } = string.Empty;
        private string userMenuDisplay = Constant.none;

        private void ToggleUserMenu()
        {
            userMenuDisplay = userMenuDisplay == Constant.none ? Constant.block : Constant.none;
        }

        public void CloseMenu()
        {
            userMenuDisplay = Constant.none;
            StateHasChanged();
        }
    }
}
