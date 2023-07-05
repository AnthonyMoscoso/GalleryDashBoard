using Bsn.Utilities.Constants;
using DashboardGallery.Shared.Services.JsFunctions.Index;
using DashboardGallery.Shared.Services.JsFunctions.Interfaces;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Components
{
    public partial class Modal
    {
        [Parameter]
        public string Title { get; set; } = string.Empty;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public string Class { get; set; } = string.Empty;
        [Parameter]
        public string Style { get; set; } = string.Empty;
        [Parameter]
        public bool IsFullScreen { get; set; } = false;
        [Inject] IJsFunctions? jsFunctions { get; set; }
        private IJsIndexFunctions? jsIndexFunctions { get; set; }
        private bool IsOpen { get; set; }
        private string ModalDisplay =>  IsOpen? "block" : "none";
        private string ModalClass => $"modal {(IsOpen ? "show" : "")} ";
        private string CssOverlay => IsOpen ? "overlay" : "";
        private string ModalStyle => $"display:{ModalDisplay}";
        private string FullSreen => IsFullScreen ? "modal-screenfull modal-fullwidth " : "modal-no-fullscreen ";
        private readonly string hiddenOverflow = "hiddenOverflow";
        protected override async Task OnInitializedAsync()
        {
            jsIndexFunctions = await jsFunctions!.Index();
        }
        public async Task Show()
        {
            IsOpen = true;
            if (IsFullScreen)
            {
                await jsIndexFunctions!.AddClassInBody(hiddenOverflow);
            }

           StateHasChanged();
        }



        public async Task Hide()
        {
            IsOpen = false;
            if (IsFullScreen)
            {
                await jsIndexFunctions!.RemoveClassInBody(hiddenOverflow);
            }
         
            StateHasChanged();
        }
    }
}
