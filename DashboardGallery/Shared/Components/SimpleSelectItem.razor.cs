using DashboardGallery.ViewModels;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Components
{
    public partial class SimpleSelectItem
    {
        [Parameter]
        public string BackgroundColor { get; set; } = "white";
        [Parameter]
        public string TextColor { get; set; } = "black";
        [Parameter]
        public int BorderSize { get; set; } = 1;
        [Parameter]
        public string SelectBorderColor { get; set; } = "black";
        [Parameter]
        public string SelectArrowHoverColor { get; set; } = "black";
        
        [Parameter]
        public SelectItemModel? Selected { get; set; }
        [Parameter]
        public ICollection<SelectItemModel> Items { get; set; } = new List<SelectItemModel>();
        [Parameter]
        public EventCallback<SelectItemModel> OnSelectValueChanged { get; set; }
        [Parameter]
        public string Class { get; set; } = string.Empty;
        [Parameter]
        public string OptionClass { get; set; } = string.Empty;
        [Parameter]
        public bool IsDisabled { get; set; } = false;

        private string Style => $"--selectBorderSize:{BorderSize}px;--selectBorderColor:{SelectBorderColor};";

        private string SelectStyle => $"--selectBackgroud:{BackgroundColor};--selectTextColor:{TextColor};--selectArrowHoverColor:{SelectArrowHoverColor}";
        private bool IsSeleted(SelectItemModel item)
        {
            return Selected != null && Selected.Key == item.Key;
        }
        private async void SelectItem_ValueChanged(ChangeEventArgs e)
        {
            object? value = e.Value;
            if (value != null)
            {
                string key = value.ToString()!;
                SelectItemModel? item = Items.FirstOrDefault(x => x.Key == key);
                await OnSelectValueChanged.InvokeAsync(item);
            }
        }
    }
}
