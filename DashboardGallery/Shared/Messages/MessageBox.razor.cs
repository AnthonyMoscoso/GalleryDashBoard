using DashboardGallery.Shared.Components;
using DashboardGallery.Shared.Messages.Model;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Messages
{
    public partial class MessageBox
    {
        private Modal modalRef = new();
        private string _message = string.Empty;
        private string _title = string.Empty;
        private string _buttonMessage = string.Empty;
        private MessageBoxConfig _messageBoxConfig = new();
        private string StyleModelContent => $"--borderColor:{_messageBoxConfig.BorderColor};--backgroundModalColor:{_messageBoxConfig.BackgroundColor}";
        private string ButtonStyle => $"--buttonColor:{_messageBoxConfig.ButtonColor};--buttonBorderColor:{_messageBoxConfig.ButtonBoderColor};--buttonBorder:{_messageBoxConfig.ButtonBoder}px;--buttonTextColor:{_messageBoxConfig.ButtonTextColor}";
        [Parameter]
        public EventCallback OnClick { get; set; }
        public async Task Show(string message, string tittle, string buttonMessage = "", MessageBoxConfig? messaBoxConfig = null )
        {
            _message = message;
            _title = tittle;
            _messageBoxConfig = messaBoxConfig ?? new MessageBoxConfig();
            _buttonMessage = buttonMessage;
            StateHasChanged();
            await modalRef.Show();
        }

        public async Task Hide()
        {
            //_messageBoxConfig = new();
            await modalRef.Hide();
        }

        private async Task OnButtonClicked()
        {
           await OnClick.InvokeAsync();
            await Hide();
        }
       


    }
}
