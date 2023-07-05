using Bsn.Authentication;
using Bsn.Utilities.Navigation.Enums;
using Bsn.Utilities.Navigation.Interfaces;
using Core.Utilities.Factories;
using DashboardGallery.Shared.Components;
using DashboardGallery.Shared.Constants;
using DashboardGallery.Shared.Errors;
using DashboardGallery.Shared.Literals;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Model.Entity;

namespace DashboardGallery.Pages
{
    public partial class Login
    {
        [Inject] IAuthentificationStateService? AuthentificationStateService { get; set; }
        [CascadingParameter]
        ErrorHandler? ErrorHandler { get; set; }
        [Inject] INavigationService? NavigationServicie { get; set; }
        [CascadingParameter]
        public LiteralsManager? Literals { get; set; }
        private string _errorMessage = string.Empty;

        private TextBox? _txtEmail = new();
        private TextBox? _txtPassword = new();
        private readonly LoginRequest _loginRequest = new();
        private bool isBtnDisabled = true;

        private async Task OnLoginClicked()
        {

            try
            {
                bool haveErrors =  CheckFails();
                if (haveErrors)
                {
                    return;
                }
                isBtnDisabled = true;
                await AuthentificationStateService!.Login(Factory.CreateFrom(_loginRequest)
                    );
                NavigationServicie?.Goto(LocalPages.Index);
            }
            catch (UnauthorizedAccessException ex)
            {
                _errorMessage = ex.Message;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                await ErrorHandler!.ProcessError(ex);
            }
            finally
            {
                isBtnDisabled = false;
            }

        }


        private bool CheckFails()
        {
            int errors = 0;
            bool isEmailEmpty = string.IsNullOrWhiteSpace(_loginRequest.Identifier);
            errors = isEmailEmpty ? errors + 1 : errors;
            _txtEmail!.SetErrorIf(isEmailEmpty, Literals!.Errors.Value_cannot_be_empty);
            bool isPassEmpty = string.IsNullOrWhiteSpace(_loginRequest.Credential);
            errors = isPassEmpty ? errors + 1 : errors;
            _txtPassword!.SetErrorIf(isEmailEmpty, Literals!.Errors.Value_cannot_be_empty);
            bool haveErrors = errors > 0;
            isBtnDisabled = haveErrors;
            return haveErrors;
        }
        private  void OnTxtEmailChanged(string value)
        {
            _loginRequest.Identifier = value;
             CheckFails();
        }

        private async void OnKeyDown(KeyboardEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Key) && e.Key.ToLower().Equals(KeyBoard.Enter.ToLower()))
            {
                await OnLoginClicked();
     
            }
     

        }
        private void OnRecoverPasswordClicked()
        {

        }
        private  void OnTxtPassChanged(string value)
        {
            _loginRequest.Credential = value;
             CheckFails();
        }

    }
}
