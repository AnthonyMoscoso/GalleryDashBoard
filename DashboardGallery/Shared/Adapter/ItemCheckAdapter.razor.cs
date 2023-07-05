using Bsn.Utilities.PlaceHolder;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace DashboardGallery.Shared.Adapter
{
    public partial class ItemCheckAdapter
    {
       
        [Parameter] public string Image { get; set; } = PlaceholderUrls.Size400;
        [Parameter] public object Item { get; set; } = new object();
        [Parameter] public bool Check { get; set; }
        [Parameter] public EventCallback<(bool,object)> OnCheckChanged { get; set; }
        [Parameter] public bool Disabled { get; set; }

        private string DisabledCss => Disabled ? "adapter-disabled" : string.Empty;
        private string Style => $"background-image: url({Image})";

        private async Task OnAdapterClick()
        {
            if (Disabled)
            {
                return;
            }
            Check = !Check;
            await OnCheckChanged.InvokeAsync((Check, Item));
        }

        private async Task OnValueChange(bool value)
        {
            if (Disabled)
            {
                return;
            }
            Check = value;
            await OnCheckChanged.InvokeAsync((value,Item));
        }
    }
}
