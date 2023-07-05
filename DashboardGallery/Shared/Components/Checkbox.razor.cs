using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Components
{
    public partial class Checkbox
    {
        [Parameter]
        public string Class { get; set; } = string.Empty;
        [Parameter]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public bool Checked { get; set; } = false;
        [Parameter]
        public EventCallback<bool> CheckedChanged { get; set; }
        [Parameter]
        public EventCallback<(bool,string)> CheckedChangedId { get; set; }
        [Parameter]
        public string Text { get; set; } = string.Empty;

        private bool _checked;

        protected override Task OnInitializedAsync()
        {
            _checked = Checked;
            StateHasChanged();

            return base.OnInitializedAsync();
        }
        protected override async Task OnParametersSetAsync()
        {
            _checked = Checked;
            StateHasChanged();
            await Task.CompletedTask;
        }
        private async Task HandleCheckboxChange(ChangeEventArgs e)
        {
            if (Disabled)
            {
                return;
            }
            _checked = (bool)e.Value!;
            Checked = _checked;
            StateHasChanged();
            await CheckedChanged.InvokeAsync(_checked);
            await CheckedChangedId.InvokeAsync((_checked, Id));
        }

    }
}
