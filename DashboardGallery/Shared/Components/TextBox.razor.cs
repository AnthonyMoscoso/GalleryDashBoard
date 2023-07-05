using Bsn.Utilities.Constants;
using DashboardGallery.Shared.Components.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DashboardGallery.Shared.Components
{
    public partial class TextBox
    {
        [Parameter]
        public EventCallback<string> OnTextChanged { get; set; }
        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter]
        public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }
        [Parameter]
        public string Text { get; set; } = string.Empty;
        [Parameter]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string Class { get; set; } = string.Empty;
        [Parameter]
        public string InputClass { get;set; } = string.Empty;
        [Parameter]
        public TextRole Role { get; set; } = TextRole.Text;
        [Parameter]
        public string Name { get; set; } = string.Empty;
        [Parameter]
        public int MaxLenght { get; set; } = int.MaxValue;
        [Parameter]
        public bool Autofocus { get; set; } = false;
        [Parameter]
        public string Placeholder { get; set; } = string.Empty;
        [Parameter]
        public string BorderColor { get; set; } = "#ccc";
        [Parameter]
        public string BoxShandonwColor { get; set; } = "#295F66";

        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public bool ReadOnly { get; set; } = false;
        private string Style => $"--inputBorderColor:{BorderTextColor};--boxShandonwColor:{BorderTextFocusColor}";
        private string Type=> Role.ToString().ToLower();
        private bool isError = false;
        private string _errorMessage = string.Empty;

        private string BorderTextColor => isError ? "#E81A00" : BorderColor;
        private string BorderTextFocusColor => isError ? "#E81A00" : BoxShandonwColor;
        private async void OnValueChange(ChangeEventArgs e)
        {
             Text = e.Value!.ToString()!;
            StateHasChanged();
            await OnTextChanged.InvokeAsync(Text);
         
        }

        /// <summary>
        /// Check is condition is true, is true draw a error message if not clean error
        /// </summary>
        /// <param name="condition">bool condition to check is error</param>
        /// <param name="errorMessage">error message to draw if condidion is success</param>
        /// <returns>not return valuid</returns>
        public void SetErrorIf(bool condition,string errorMessage)
        {
            if (condition)
            {
                 SetError(errorMessage);
                return;
            }
             CleanError();
        }

        
        public void  SetError(string errorMessage)
        {

            isError = true;
            StateHasChanged();
            _errorMessage = errorMessage;
        }
        public void CleanError()
        {
            _errorMessage = string.Empty;
            isError = false;
            StateHasChanged();
        }
        private async void OnKeyDownHandler(KeyboardEventArgs eventArgs)
        {
           await  OnKeyDown.InvokeAsync(eventArgs);
        }
    
        public void SetText(string text)
        {
            Text = text;
            StateHasChanged();
        }
        private async Task OnClickHandler(MouseEventArgs e)
        {
            await OnClick.InvokeAsync(e);
        }
      
    }
}
