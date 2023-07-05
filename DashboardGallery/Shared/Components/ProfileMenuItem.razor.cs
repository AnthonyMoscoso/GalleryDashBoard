using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Components
{
    public partial class ProfileMenuItem :ComponentBase
    { 
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter] 
        public string Class { get; set; } = string.Empty;
    }
}
