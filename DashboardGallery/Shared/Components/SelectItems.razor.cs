using DashboardGallery.ViewModels;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Components
{
    public partial class SelectItems 
    {
        [Parameter]
        public ICollection<SelectItemModel> Items { get; set; } = new List<SelectItemModel>();
        [Parameter]
        public SelectItemModel? SelectedItem { get; set; }
        [Parameter]
        public EventCallback<SelectItemModel> OnSelectValueChanged { get; set; }
        [Parameter]
        public string Class { get; set; } = string.Empty;
        [Parameter]
        public string OptionClass { get; set; } = string.Empty;

        private bool IsSeleted(SelectItemModel item)
        {
            return SelectedItem != null && SelectedItem.Key == item.Key ;
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
