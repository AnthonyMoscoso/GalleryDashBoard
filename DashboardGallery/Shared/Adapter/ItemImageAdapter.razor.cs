using Bsn.Utilities.PlaceHolder;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Adapter
{
    public partial class ItemImageAdapter
    {
        [Parameter] public string Image { get; set; } = PlaceholderUrls.Size400;
        [Parameter] public object Item { get; set; } = new object();
        [Parameter] public EventCallback<object> OnViewClicked { get; set; }
        [Parameter] public EventCallback<object> OnDeleteClicked { get; set; }
        [Parameter] public EventCallback<object> OnEditClicked { get; set; }

        private string Style => $"background-image: url({Image})";

        private async void OnBtnPreviewClick()
        {
            await OnViewClicked.InvokeAsync(Item);
        }

        private async void OnBtnEditClick()
        {
            await OnEditClicked.InvokeAsync(Item);
        }

        private async void OnBtnDeleteClick()
        {
            await OnDeleteClicked.InvokeAsync(Item);
        }
    }
}
