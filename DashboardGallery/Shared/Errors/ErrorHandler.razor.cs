using Bsn.Authentication;
using DashboardGallery.Shared.Messages;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Errors
{
    public partial class ErrorHandler
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }

        [CascadingParameter] public MessageHandler MessageHandler { get; set; } = new MessageHandler();
        [Inject] IAuthentificationStateService?  AuthentificationStateService { get; set; }

        public async Task ProcessError(Exception ex)
        {

            string? errorType = ex.GetType().Name;
            string message = ex.Message;
            if (errorType.ToLower().Equals("UnauthorizedAccessException".ToLower()) || errorType.ToLower().Equals("UnathorizedException".ToLower()))
            {
                AuthentificationStateService?.RedirectToLogin();
                return;
            }
            await MessageHandler!.ShowMessage(message,"Error", messageTypes: Messages.Enums.MessageTypes.warning);
            StateHasChanged();
        }
    }
}
