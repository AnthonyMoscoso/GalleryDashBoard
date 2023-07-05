using Bsn.Utilities.Navigation.Enums;
using Bsn.Utilities.Navigation.Interfaces;
using DashboardGallery.Shared.Literals;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared
{
    public partial class NavMenu
    {

        [CascadingParameter] LiteralsManager? Literals { get; set; }
        [Inject] INavigationService? NavigationServicie { get; set; }

        private LocalPages selectPage = LocalPages.Index;
        
        private string GetCssActive(LocalPages localPages)
        {
            if (selectPage != localPages)
            {
                return string.Empty;
            }
            return "nav-active";
        }
        private void GoTo(LocalPages localPages)
        {
            selectPage = localPages;
            StateHasChanged();
            NavigationServicie!.Goto(selectPage);
           
        }


    }

}
