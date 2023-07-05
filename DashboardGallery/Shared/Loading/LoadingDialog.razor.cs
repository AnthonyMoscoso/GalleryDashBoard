
using DashboardGallery.Shared.Components;
using DashboardGallery.Shared.Messages.Model;
using DashboardGallery.Shared.Waiting.Model;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Loading
{
    public partial class LoadingDialog
    {
        private Modal modalRef = new();
        private string _title = string.Empty;
        private LoadingDialogConfig _config = new();
        private string StyleModelContent => $"--borderLoadingModalColor:{_config.ModalBorderColor};--backgroundLoadingModalColor:{_config.ModalBackgroundColor};--borderSizeLoadingModal:{_config.ModalBorderSize}px;--borderRadiusLoadingModal:{_config.ModalBorderRadius}px";
        [Parameter]
        public EventCallback OnClick { get; set; }
        public async Task Show( string tittle = "", LoadingDialogConfig? config = null)
        {
           
            _title = tittle;
            _config = config ?? new ();
            StateHasChanged();
            await modalRef.Show();
        }

        public async Task Hide()
        {
            await modalRef.Hide();
        }

    }
}
