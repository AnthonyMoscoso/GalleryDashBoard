using Bsn.Utilities.Constants;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Auth
{
    public partial class RedirectToLogin
    {
        [Inject] NavigationManager? Navigation { get; set; }
        protected override void OnInitialized()
        {
            Navigation?.NavigateTo($"{NavUrl.Login}");
        }
    }
}
