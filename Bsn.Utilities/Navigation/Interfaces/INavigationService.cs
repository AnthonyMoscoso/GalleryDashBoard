using Bsn.Utilities.Navigation.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Bsn.Utilities.Navigation.Interfaces
{
    public interface INavigationService
    {
        void Goto([NotNull]LocalPages localPages,string? id = null, IDictionary<string,string>? parameters = null);
       
    }
}
