using Bsn.Utilities.LocalStorage.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using DashboardGallery.Shared.Errors;
using Bsn.Utilities.Token.Interfaces;

namespace DashboardGallery.Shared.Auth
{
    public partial class AuthenticationStateDisplay
    {
        #region Authenticate
        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }
        private AuthenticationState? AuthState { get; set; }
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Inject] public ILocalStorageService? LocalStorageService { get; set; }
        [Inject] public ITokenService? TokenService { get; set; }
        #endregion
        [CascadingParameter] private ErrorHandler? Error { get; set; }
        protected override async Task OnInitializedAsync()
        {

            try
            {
                AuthState = await AuthenticationState!;
                bool? isAuthenticated = AuthState.User.Identity?.IsAuthenticated;
                if (!isAuthenticated.HasValue || !isAuthenticated.Value)
                {
                    throw new UnauthorizedAccessException();
                }
                DateTime now = DateTime.UtcNow;



            }
            catch (Exception ex)
            {
                await Error!.ProcessError(ex);
            }

        }
    }
}

