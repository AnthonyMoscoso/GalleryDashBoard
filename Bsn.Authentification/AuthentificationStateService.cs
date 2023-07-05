using Bsn.Authentication;
using Bsn.DataServices.Interfaces;
using Bsn.Utilities.Constants;
using Bsn.Utilities.Navigation.Enums;
using Bsn.Utilities.Navigation.Interfaces;
using Bsn.Utilities.Token.Interfaces;
using Core.Utilities;
using Core.Utilities.Ensures;
using Microsoft.AspNetCore.Components.Authorization;
using Model.Entity;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Bsn.Authentification
{
    public class AuthentificationStateService : AuthenticationStateProvider, IAuthentificationStateService
    {



        private readonly IAuthServices _authServices;
        private readonly ITokenService _tokenService;
        private static AuthenticationState Anonimous => new(new ClaimsPrincipal(new ClaimsIdentity()));
        private readonly INavigationService _navigationService;
       
        public AuthentificationStateService(ITokenService tokenService, IAuthServices authServices, INavigationService navigationService)
        {
            _authServices = authServices;
            _tokenService = tokenService;
            _navigationService = navigationService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string? token = await _tokenService.GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Anonimous;
            }
            JwtSecurityToken jwtToken = new(token);
            DateTime expirationTime = jwtToken.ValidTo;
            if (IsExpirationToken(expirationTime))
            {
                await LogOut();

            }
            return BuildAuthenticationState(token);
        }

     
        private static bool IsExpirationToken(DateTime expirationTime)
        {
            return expirationTime <= DateTime.UtcNow;
        }


      

        public async Task Login([NotNull] LoginRequest loginRequest)
        {
            Ensure.That(loginRequest, nameof(loginRequest)).IsNotNull();
            string token = await _authServices.Login(loginRequest);
            if (string.IsNullOrEmpty(token))
            {
                return;
            }
            await _tokenService.Register(token);
            loginRequest.Identifier = Base64.Encode(loginRequest.Identifier);
            loginRequest.Credential = Base64.Encode(loginRequest.Credential);
            AuthenticationState? authState = BuildAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));

        }

        public async Task LogOut()
        {
            try
            {
                await _authServices.Logout();
                await _tokenService.CleanToken();
                NotifyAuthenticationStateChanged(Task.FromResult(Anonimous));
                RedirectToLogin();
            }
            catch (Exception) 
            {
                RedirectToLogin();
            }

        }


      

        private static AuthenticationState BuildAuthenticationState(string token)
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), Constant.JWT)));
        }
        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            JwtSecurityToken jwtToken = new(jwt);
            return jwtToken.Claims;
        }

        public void RedirectToLogin()
        {
            _navigationService.Goto(LocalPages.Login);
        }
    }
}
