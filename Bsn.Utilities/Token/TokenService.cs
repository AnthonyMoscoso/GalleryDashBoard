using Bsn.Utilities.Constants;
using Bsn.Utilities.LocalStorage.Interfaces;
using Bsn.Utilities.Token.Enums;
using Bsn.Utilities.Token.Interfaces;
using Core.Utilities.Ensures;
using Core.Utilities.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;

namespace Bsn.Utilities.Token
{
    public class TokenService : ITokenService
    {
        private readonly ILocalStorageService _localStorageService;
        public TokenService(ILocalStorageService localStorageService) 
        { 
            _localStorageService = localStorageService;
        }

        public async Task<string?> GetToken()
        {
            string? token = await _localStorageService.GetValue<string>(Constant.Token);
            return token;
        }

        public async Task CleanToken()
        {
            await _localStorageService.Remove(Constant.Token);
        }

        public async Task<TValue?> GetValue<TValue>([NotNull]string key)
        {
            string? token = await GetToken();
            if (token == null)
            {
                return default;
            }
            JwtSecurityToken jwtSecurityToken = new(token);
            return jwtSecurityToken.GetClaimValue<TValue>(key);
        }

        public async Task<TValue?> GetValue<TValue>([NotNull] TokenKeys tokenKeys)
        {
            string? token = await GetToken();
            if (token == null)
            {
                return default;
            }
            JwtSecurityToken jwtSecurityToken = new(token);
            string? key = TokenDatas.TokenKeys.GetValueOrDefault(tokenKeys);
            if (string.IsNullOrWhiteSpace(key))
            {
                return default;
            }
            return jwtSecurityToken.GetClaimValue<TValue>(key);
        }
        public async Task Register([NotNull]string token)
        {
            Ensure.That(token, nameof(token)).NotNullOrEmpty();
            await _localStorageService.AddOrUpdateValue(Constant.Token, token);
        }

        public Task<DateTime> GetExpirationDate()
        {
            throw new NotImplementedException();
        }
    }
}
