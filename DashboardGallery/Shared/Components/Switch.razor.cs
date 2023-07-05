using DashboardGallery.ViewModels;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Components
{
    public partial class Switch
    {
        [Parameter]
        public string Class { get; set; } = string.Empty;
        [Parameter]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public bool Value { get; set; }
        [Parameter]
        public Color CheckColor { get; set; } = Color.LimeGreen;
        [Parameter]
        public Color UnCheckColor { get; set; } = Color.LightGray;
        [Parameter]
        public EventCallback<bool> OnValueChanged { get; set; }
        [Parameter]
        public EventCallback<(bool, string)> CheckedChangedId { get; set; }
        private bool _checked;

        private string Style => $"--toggleCheckColor:{CheckColor};--toggleUncheckColor:{UnCheckColor}";
        protected override Task OnInitializedAsync()
        {
            _checked = Value;
            StateHasChanged();

            return base.OnInitializedAsync();
        }
        protected override async Task OnParametersSetAsync()
        {
            _checked = Value;
            StateHasChanged();
            await Task.CompletedTask;
        }
        private async Task HandleCheckboxChange(ChangeEventArgs e)
        {
            _checked = (bool)e.Value!;
            Value = _checked;
            StateHasChanged();
            await OnValueChanged.InvokeAsync(_checked);
            await CheckedChangedId.InvokeAsync((_checked, Id));
        }
    }
}
