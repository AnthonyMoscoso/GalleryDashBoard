using Bsn.Utilities.PlaceHolder;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Adapter
{
    public partial class ItemAdapterRemoved
    {
        [Parameter] public string Image { get; set; } = PlaceholderUrls.Size400;
        [Parameter] public object Item { get; set; } = new object();

        [Parameter] public EventCallback<object> OnDeleteClicked { get; set; }

        private async void OnBtnDeleteClick()
        {
            await OnDeleteClicked.InvokeAsync(Item);
        }
        private string Style => $"background-image: url({Image})";
    }
}
