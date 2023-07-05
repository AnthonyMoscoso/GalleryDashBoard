using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Components
{
    public partial class Tooltip
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public string Text { get; set; } = string.Empty;

        [Parameter] public string BackgroundColor { get; set; } = "#363636";
    }
}
