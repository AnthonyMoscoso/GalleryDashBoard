using Model.Entity;

namespace Bsn.DataServices.Interfaces
{
    public interface IAuthServices
    {
        Task<string> Login (LoginRequest loginRequest);
        Task<bool> Logout();
    }
}
