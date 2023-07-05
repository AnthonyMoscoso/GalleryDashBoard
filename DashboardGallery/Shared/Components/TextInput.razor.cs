using DashboardGallery.Shared.Components.Enums;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Components
{
    public partial class TextInput
    {
        [Parameter] public string CssClass { get; set; } = string.Empty;
        [Parameter] public string Id { get; set;} = string.Empty;

        [Parameter] public string Tittle { get; set; } = string.Empty;
        [Parameter] public string PlaceHolder { get; set; } = string.Empty;

        [Parameter] public TextRole Role { get; set; } =    TextRole.Text;

        [Parameter] public bool Disable { get; set; }

        [Parameter] public EventCallback<string> OnTextChanged { get; set; }
   
        [Parameter] public string Text { get; set; } = string.Empty;

        [Parameter] public bool IsReadOnly { get; set; } = false;
        [Parameter] public bool Autofocus { get; set; } = false;
        [Parameter] public int MaxLenghtText { get; set; } = int.MaxValue;
        private TextRole TextRole { get; set; }
        private bool moveUp = false;
        private bool blind = false;
        private string _errorMessage = string.Empty;
        private string IconName => blind ? "fa-eye-slash" :"fa-solid fa-eye" ;
    
        private string LabelCss => moveUp || Disable || !string.IsNullOrEmpty(Text) ? "Dash-form-label Dash-label-move-up" : "Dash-form-label";
        private string DisableCss => Disable ? "component-disable" : string.Empty;
        private string NoneIcon => Role == TextRole.Password ? "icon-error" : string.Empty;
        protected override async void OnInitialized()
        {
            if (Role == TextRole.Password)
            {
                TextRole = TextRole.Password;
            }
            await Task.CompletedTask;
        }
        private async Task Text_TextChanged(string value)
        {
            Text = value;
            moveUp = !string.IsNullOrEmpty(value);
            await OnTextChanged.InvokeAsync(value);
        }
        private void OnFocusOut()
        {
            moveUp = !string.IsNullOrEmpty(Text);
        }
        private void TextClick()
        {
            moveUp = true;
        }

        public void CleanError()
        {
         
            _errorMessage = string.Empty;
        }
        public void SetError(string error)
        {
           
            _errorMessage = error;


        }

        private void BtnEyeOnClick()
        {
            if (TextRole == TextRole.Password)
            {
                blind = true;
                TextRole = TextRole.Text;
            }
            else
            {
                blind = false;
                TextRole = TextRole.Password;
            }
            StateHasChanged();
        }

    }
}
