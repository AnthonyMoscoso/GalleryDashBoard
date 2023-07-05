using DashboardGallery.ViewModels;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Components
{
    public partial class TabPanel
    {
        
        [Parameter]
        public EventCallback<int> OnStepClick { get; set; }
        [Parameter]
        public int? SelectedItem { get; set; }
        [Parameter]
        public string Class { get; set; } = string.Empty;
        [Parameter]
        public Color Color { get; set; } = Color.LightGray; 
        [Parameter]
        public RenderFragment? TabHeaders { get; set; }

        private string Style => $"--stepPanelBorderBottonColor:{Color}";

    }
}
