using Bsn.Utilities.PlaceHolder;
using Microsoft.AspNetCore.Components;
using static System.Net.Mime.MediaTypeNames;

namespace DashboardGallery.Shared.Adapter
{
    public partial class ItemNameAdapter
    {
        [Parameter] public string Image { get; set; } = PlaceholderUrls.Size400;
        [Parameter]
        public string Name { get; set; } = string.Empty;
        [Parameter]
        public string SubText { get; set; } = string.Empty;
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
