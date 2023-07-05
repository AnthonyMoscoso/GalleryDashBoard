using DashboardGallery.Shared.Literals;
using Microsoft.AspNetCore.Components;

namespace DashboardGallery.Shared.Components
{
    public partial class Paginator
    {
        [Parameter]
        public int PageItems { get; set; }
        [Parameter]
        public EventCallback<int> SendPage { get; set; }
        [CascadingParameter] LiteralsManager? Literals { get; set; }
        private string CurrentPage { get; set; } = 1.ToString();

        private bool IsActive(string page)
            =>  CurrentPage == page;

        private bool IsPageNavigationDisabled(string navigation)
        {
            if (!string.IsNullOrEmpty(navigation) && !string.IsNullOrWhiteSpace(navigation))
            {
                if (navigation.Equals(Literals!.Previous))
                {
                    return CurrentPage.Equals(1.ToString()) || PageItems <2  ;
                }
                else if (navigation.Equals(Literals.Next))
                {

                    return CurrentPage.Equals(PageItems.ToString()) || PageItems <2;
                }
               
            }
            return false;
        }

    
        public void Refresh()
        {
            StateHasChanged();
        }
        public void Reset()
        {
            CurrentPage = 1.ToString();
            StateHasChanged();
        }
        private void Previous()
        {
            var currentPageAsInt = int.Parse(CurrentPage);
            if (currentPageAsInt > 1)
            {
                CurrentPage = (currentPageAsInt - 1).ToString();
                currentPageAsInt = int.Parse(CurrentPage);
                SendPage.InvokeAsync(currentPageAsInt);
            }
     
        }

        private void Next()
        {
            var currentPageAsInt = int.Parse(CurrentPage);
            if (currentPageAsInt < PageItems)
            {
                CurrentPage = (currentPageAsInt + 1).ToString();
                currentPageAsInt = int.Parse(CurrentPage);
                SendPage.InvokeAsync(currentPageAsInt);
            }
     
        }

        private void SetActive(string page)
        {
            int actualPage = Convert.ToInt32(page);
            CurrentPage = page;
            SendPage.InvokeAsync(actualPage);
        }

   
    }
}
