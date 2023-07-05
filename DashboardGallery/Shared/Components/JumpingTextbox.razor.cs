using DashboardGallery.Shared.Components.Enums;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Components
{
    public partial class JumpingTextbox
    {
        [Parameter]
        public TextRole Role { get; set; } = TextRole.Text;
        [Parameter] public string CssClass { get; set; } = string.Empty;
        [Parameter] public string Id { get; set; } = string.Empty;
        [Parameter] public string Tittle { get; set; } = string.Empty;
        [Parameter] public string Name { get; set; } = string.Empty;
        [Parameter] public bool Disable { get; set; }
        [Parameter] public EventCallback<string> OnTextChanged { get; set; }
        [Parameter] public string Text { get; set; } = string.Empty;
        [Parameter] public bool IsReadOnly { get; set; } = false;
        [Parameter] public bool Autofocus { get; set; } = false;
        [Parameter] public int MaxLenghtText { get; set; } = int.MaxValue;
        [Parameter] public string BorderFocusColor { get; set; } = "black";

        private string BorderTextFocusColor => isError ? "red" : BorderFocusColor;
        private string defaultBorderColor = "#dddddd";
        private bool isError = false;
        private string StyleInput => isError? $"--textborderColor:red;" : $"--textborderColor:{defaultBorderColor};";
        private string borderColor => defaultBorderColor;
        private string Style => $"--JumpingTextboxBorderColor:{BorderTextFocusColor};{StyleInput}";
        private string Type => Role.ToString().ToLower();
        private string _errorMessage = string.Empty; 

        public async Task SetError(string errorMessage)
        {
           
            isError = true;
            StateHasChanged();
            _errorMessage = errorMessage;
            await  Task.CompletedTask;
        }

        public async Task CleanError()
        {
            _errorMessage = string.Empty;
            isError = false;
            StateHasChanged();
            await Task.CompletedTask;
        }
        private async void OnValueChange(ChangeEventArgs e)
        {
            Text = e.Value!.ToString()!;
            StateHasChanged();
            await OnTextChanged.InvokeAsync(Text);

        }
    }
}
