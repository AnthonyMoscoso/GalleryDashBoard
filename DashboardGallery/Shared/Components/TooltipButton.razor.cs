using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Components
{
    public partial class TooltipButton
    {
       
        private string optionsPanelStyle = "";
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public string Width { get; set; } = "100%";
        [Parameter]
        public string Css { get; set; } = string.Empty;
        [Parameter]
        public EventCallback OnDeleteClicked { get; set; }
        [Parameter]
        public EventCallback OnEditClicked { get; set; }


        private async Task DeleteElement()
        {
            await OnDeleteClicked.InvokeAsync();
          
        }

        private async Task EditElement()
        {
            await OnEditClicked.InvokeAsync();
        }
    }
}
