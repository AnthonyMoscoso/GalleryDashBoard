using AutoMapper;
using Bsn.DataServices.Interfaces;
using Bsn.RestServices;
using Bsn.RestServices.Models;
using Bsn.Utilities.Constants;
using Bsn.Utilities.Token.Interfaces;
using Core.Utilities;
using Model.Dto;
using Model.Entity;
using Model.Entity.Auth;
using System.Text.Json;

namespace Bsn.DataServices
{
    public class AuthService : IAuthServices
    {
        private readonly IRest _rest;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public AuthService(IRest rest,IMapper mapper, ITokenService tokenService)
        {
            _rest = rest;
            _mapper = mapper;
            _tokenService = tokenService;
          
        }
        public async Task<string> Login(LoginRequest loginRequestView)
        {
            LoginRequest loginRequest = _mapper.Map<LoginRequest>(loginRequestView);
            string uri = $"{ApiUrls.Auth}";
            loginRequest.Credential = Base64.Encode(loginRequest.Credential);
            loginRequest.Identifier = Base64.Encode(loginRequest.Identifier);
            RestResult restResult = await  _rest.Send(uri,loginRequest); 
            if (restResult.HttpStatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                ErrorResult? errorResult = JsonSerializer.Deserialize<ErrorResult>(restResult.Result);
                throw new Exception(errorResult!.Message);
            }
            if (restResult.HttpStatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                ErrorResult? errorResult = JsonSerializer.Deserialize<ErrorResult>(restResult.Result);
                throw new UnauthorizedAccessException(errorResult!.Message);
            }
            if (restResult.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            }
            LoginResult? loginResult = JsonSerializer.Deserialize<LoginResult>(restResult.Result);
            return loginResult == null ? throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR) : loginResult.Token;
        }

        public async Task<bool> Logout()
        {
            string uri = $"{ApiUrls.LogOut}";
            string? token = await _tokenService.GetToken();
            RestResult restResult = await _rest.Get(uri, token!);
            if (restResult.HttpStatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                ErrorResult? errorResult = JsonSerializer.Deserialize<ErrorResult>(restResult.Result);
                throw new Exception(errorResult!.Message);
            }
            if (restResult.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            }
            SuccessResult? successResult = JsonSerializer.Deserialize<SuccessResult>(restResult.Result);
            return successResult == null ? throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR) : successResult.Value;
        }
    }
}
