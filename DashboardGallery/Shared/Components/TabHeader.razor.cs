using DashboardGallery.Shared.Constants;
using DashboardGallery.ViewModels;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace DashboardGallery.Shared.Components
{
    public partial class TabHeader
    {
        [Parameter]
        public Color SelectColor { get; set; } = Color.Primary;
        [Parameter]
        public string Class { get; set; } = string.Empty;
        [Parameter]
        [Required]
        public string Name { get; set; } = string.Empty;
        [Parameter]
        [Required]
        public int Value { get; set; }
        [Parameter]
        public EventCallback<int> OnTabClick { get; set; }
        [Parameter]
        public bool IsSelected { get; set; }
        private string GetActiveCss => IsSelected ? CssClass.tab_active : string.Empty;

        private string Style => $"--tabItemSelectColor:{SelectColor}";
        private async Task OnTabClicked()
        {
            await OnTabClick.InvokeAsync(Value);
        }
    }
}
