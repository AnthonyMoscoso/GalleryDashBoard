using DashboardGallery.Shared.Messages;
using DashboardGallery.Shared.Waiting.Model;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Loading
{
    public partial class LoadingHandler
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }
        private LoadingDialog LoadingDialog = new();

        public async Task Show(string tittle = "" ,LoadingDialogConfig? loadingDialogConfig = null)
        {
            if (loadingDialogConfig == null)
            {
                if (string.IsNullOrWhiteSpace(tittle))
                {
                    loadingDialogConfig = new()
                    {
                        ImageUrl = "https://media0.giphy.com/media/TLnWsIBRegQyWxG4Dw/giphy.gif?cid=ecf05e47vdtg3gy99ejfz5n74dhgg3bhsbuhe9pqyt4el6h1&ep=v1_gifs_search&rid=giphy.gif&ct=g"
                    };
                }
                else
                {
                     loadingDialogConfig = new()
                    {
                        ModalBackgroundColor = "white",
                        ModalMessageColor = "#4F368E",
                    };
                }
            }
           
          
            await LoadingDialog.Show(tittle,loadingDialogConfig);
        }

        public async Task Hide()
        {
            await LoadingDialog.Hide();
        }
    }
}
