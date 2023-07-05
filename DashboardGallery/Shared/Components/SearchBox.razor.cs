using DashboardGallery.Shared.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json.Linq;

namespace DashboardGallery.Shared.Components
{
    public partial class SearchBox
    {
        [Parameter]
        public EventCallback<string> OnTextChanged { get; set; }
        [Parameter]
        public string Css { get; set; } = string.Empty;
        [Parameter]
        public string PlaceHolder { get; set; } = "search";
        [Parameter]
        public string FocusColor { get;set; } = "black";
        [Parameter]
        public bool Autofocus { get; set; } = false;
        private string searchTerm = string.Empty;
        private bool hiddenClear = true;


        private async Task Search()
        {
            await OnTextChanged.InvokeAsync(searchTerm);
        }
        private void OnValueChanged(ChangeEventArgs e)
        {
            string? value = e.Value?.ToString();
            searchTerm = value ?? string.Empty;
            hiddenClear = string.IsNullOrWhiteSpace(value);
        }

        private async void OnKeyDown(KeyboardEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Key) && e.Key.ToLower().Equals(KeyBoard.Enter.ToLower()))
            {
               await Search();
            }
        }
        private async Task ClearSearch()
        {
            searchTerm = string.Empty;
            hiddenClear = true;
            await OnTextChanged.InvokeAsync(searchTerm);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            searchTerm = string.Empty;
        }
    }
}
