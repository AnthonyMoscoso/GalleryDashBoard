using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DashboardGallery.Shared.Components
{
    public partial class TextBoxArea : ComponentBase
    {
        [Parameter]
        public EventCallback<string> OnTextChanged { get; set; }
        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter]
        public string Text { get; set; } = string.Empty;
        [Parameter]
        public string PlaceHolder { get; set; } = string.Empty;
        [Parameter]
        public int MinHeight { get; set; } = 100;
        [Parameter]
        public int MaxLength { get; set; } = int.MaxValue;
        private string Style => $"min-height:{MinHeight}px";
        private string StyleArea => $"min-height:{MinHeight-40}px";
        private async void OnValueChange(ChangeEventArgs e)
        {
            Text = e.Value!.ToString()!;
            StateHasChanged();
            await OnTextChanged.InvokeAsync(Text);

        }
        private async Task OnClickHandler(MouseEventArgs e)
        {
            await OnClick.InvokeAsync(e);
        }

    }
}
