using Model.Entity;
using System.Diagnostics.CodeAnalysis;

namespace Bsn.Authentication
{
    public interface IAuthentificationStateService
    {
        Task Login([NotNull] LoginRequest loginRequest);
        void RedirectToLogin();
        Task LogOut();
    }
}
