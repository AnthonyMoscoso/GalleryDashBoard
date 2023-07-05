using Bsn.Utilities.Navigation.Enums;
using Bsn.Utilities.Navigation.Interfaces;
using Core.Utilities.Ensures;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;

namespace Bsn.Utilities.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly NavigationManager _navigationManager;
        public NavigationService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public void Goto([NotNull] LocalPages localPages,string? id = null, IDictionary<string, string>? parameters = null)
        {
            Ensure.That(localPages, nameof(localPages)).IsNotNull();
            string? uri = PagesValue.GetValueOrDefault(localPages);
            if (!string.IsNullOrWhiteSpace(id))
            {
                uri += $"/{id}";
            }
            if (!parameters.IsNullOrEmpty())
            {
                uri += "?";
                for (int i =0;i<parameters!.Count;i++)
                {
                    KeyValuePair<string,string> keyValuePair = parameters.ElementAtOrDefault(i);
                    string value = $"{keyValuePair.Key}={keyValuePair.Value}";
                    uri += value;
                    if (parameters.Count>1 && i+1<parameters.Count)
                    {
                        uri += "&";
                    }
                }
            }
            _navigationManager.NavigateTo(uri!);
        }


        private readonly IReadOnlyDictionary<LocalPages, string> PagesValue = new Dictionary<LocalPages, string>()
        {
            {LocalPages.Index ,string.Empty },
            {LocalPages.Login ,LocalPages.Login.ToString().ToLower()},
            {LocalPages.Images ,LocalPages.Images.ToString().ToLower() },
                        {LocalPages.Albums ,LocalPages.Albums.ToString().ToLower() },
        };

    }
}
