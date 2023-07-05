using Bsn.Utilities.PlaceHolder;
using DashboardGallery.Shared.Components;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Modals
{
    public partial class PreviewImageModal
    {
        private Modal Modal = new();
        private string Image { get; set; } = PlaceholderUrls.Size900;

        private string Style => $"background-image:url({Image})";
        public async Task Show(string image)
        {
            Image = image;
            await Modal.Show();
        }

        public async Task Close()
        {
            
            StateHasChanged();
            await Modal.Hide();
        }
    }
}
