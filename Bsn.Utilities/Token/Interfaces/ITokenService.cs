using Bsn.Utilities.Token.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Bsn.Utilities.Token.Interfaces
{
    public interface ITokenService
    {
        Task Register([NotNull]string token);
        Task<TValue?> GetValue<TValue>([NotNull]string key);
        Task<TValue?> GetValue<TValue>([NotNull] TokenKeys key);
        Task<string?> GetToken();
        Task CleanToken();
        Task<DateTime> GetExpirationDate();

    }
}
