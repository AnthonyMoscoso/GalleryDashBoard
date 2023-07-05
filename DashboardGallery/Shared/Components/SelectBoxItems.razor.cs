using DashboardGallery.ViewModels;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Components
{
    public partial class SelectBoxItems
    {
        [Parameter]
        public string Tittle { get; set; } = string.Empty;
        [Parameter]
        public SelectItemModel? Selected { get; set; }
        [Parameter]
        public ICollection<SelectItemModel> Items { get; set; } = new List<SelectItemModel>();
        [Parameter]
        public EventCallback<SelectItemModel> OnSelectValueChanged { get; set; }


        private async void SelectItem_ValueChanged(SelectItemModel item)
        {
            await OnSelectValueChanged.InvokeAsync(item);
        }
     
    }
}
