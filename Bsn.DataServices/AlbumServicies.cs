using Bsn.DataServices.Interfaces;
using Bsn.RestServices.Models;
using Bsn.RestServices;
using Bsn.Utilities.Constants;
using Core.Utilities.Exceptions;
using Model.Dto;
using Model.Dto.Table;
using Model.Entity.ImageFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Bsn.Utilities.Token.Interfaces;
using Model.Entity.Albums;
using Core.Utilities.Ensures;

namespace Bsn.DataServices
{
    public class AlbumServicies : IAlbumServices
    {
        private readonly IRest _rest;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AlbumServicies(IRest rest, ITokenService tokenService, IMapper mapper)
        {
            _rest = rest;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<SuccessResult> AddImage(string idAlbum, AlbumImageDto albumImageDto)
        {
            string uri = $"{ApiUrls.Albums}/{idAlbum}/images";
            string? token = await _tokenService.GetToken();
            UnathorizedException.ThrowIfTrue(string.IsNullOrWhiteSpace(token));
            AlbumsImageInput input = _mapper.Map<AlbumsImageInput>(albumImageDto);
            RestResult restResult = await _rest.Send(uri, input, token: token!);
            if (restResult.HttpStatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            if (restResult.HttpStatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                ErrorResult? errorResult = JsonSerializer.Deserialize<ErrorResult>(restResult.Result);
                throw new Exception(errorResult!.Message);
            }
            if (restResult.HttpStatusCode != System.Net.HttpStatusCode.Created)
            {
                throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            }
            SuccessResult? result = JsonSerializer.Deserialize<SuccessResult>(restResult.Result) ?? throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            return result;
        }

        public async Task<DataTableInfo<AlbumsDto>> DataTable(TableModel tableModel, string? search = null,bool? all = false)
        {
            string uri = $"{ApiUrls.Albums}?search={search}&take={tableModel.Take}&skip={tableModel.Skip}&orderBy={tableModel.Sorted}&isAsc={tableModel.IsAsc}&all={all}";
            string? token = await _tokenService.GetToken();
            UnathorizedException.ThrowIfTrue(string.IsNullOrWhiteSpace(token));
            RestResult restResult = await _rest.Get(uri, token!);
            if (restResult.HttpStatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            if (restResult.HttpStatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                ErrorResult? errorResult = JsonSerializer.Deserialize<ErrorResult>(restResult.Result);
                throw new Exception(errorResult!.Message);
            }
            if (restResult.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            }
            DataTableInfo<AlbumsResult>? rolResult = JsonSerializer.Deserialize<DataTableInfo<AlbumsResult>>(restResult.Result) ?? throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            DataTableInfo<AlbumsDto> dataTableInfo = new()
            {
                TotalItems = rolResult.TotalItems,
                Items = _mapper.Map<IEnumerable<AlbumsDto>>(rolResult.Items)
            };

            return dataTableInfo;
        }

        public async Task<SuccessResult> Delete(string id)
        {
            Ensure.That(id, nameof(id)).NotNullOrEmpty();
            string uri = $"{ApiUrls.Albums}";
            string? token = await _tokenService.GetToken();
            UnathorizedException.ThrowIfTrue(string.IsNullOrWhiteSpace(token));
            RestResult restResult = await _rest.Send(uri, id, RestServices.Enums.RequestMethods.Delete, token: token!);
            if (restResult.HttpStatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            if (restResult.HttpStatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                ErrorResult? errorResult = JsonSerializer.Deserialize<ErrorResult>(restResult.Result);
                throw new Exception(errorResult!.Message);
            }
            if (restResult.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            }
            SuccessResult? result = JsonSerializer.Deserialize<SuccessResult>(restResult.Result) ?? throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            return result;
        }

        public async Task<AlbumsDto> GetDetail(string id)
        {
            string uri = $"{ApiUrls.Albums}/{id}";
            string? token = await _tokenService.GetToken();
            UnathorizedException.ThrowIfTrue(string.IsNullOrWhiteSpace(token));
            RestResult restResult = await _rest.Get(uri, token!);
            if (restResult.HttpStatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            if (restResult.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            }
            AlbumsResult? datas = JsonSerializer.Deserialize<AlbumsResult>(restResult.Result) ?? throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            AlbumsDto dto = _mapper.Map<AlbumsDto>(datas);
            return dto;
        }

        public async Task<AlbumsDto> Insert(AlbumsDto item)
        {
            string uri = $"{ApiUrls.Albums}";
            string? token = await _tokenService.GetToken();
            UnathorizedException.ThrowIfTrue(string.IsNullOrWhiteSpace(token));
            AlbumsInput input = _mapper.Map<AlbumsInput>(item);
            RestResult restResult = await _rest.Send(uri, input, token: token!);
            if (restResult.HttpStatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            if (restResult.HttpStatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                ErrorResult? errorResult = JsonSerializer.Deserialize<ErrorResult>(restResult.Result);
                throw new Exception(errorResult!.Message);
            }
            if (restResult.HttpStatusCode != System.Net.HttpStatusCode.Created)
            {
                throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            }
            AlbumsResult? result = JsonSerializer.Deserialize<AlbumsResult>(restResult.Result) ?? throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            return _mapper.Map<AlbumsDto>(result);
        }

        public async Task<SuccessResult> RemoveImage(string idAlbum, string idImage)
        {
            Ensure.That(idAlbum, nameof(idAlbum)).NotNullOrEmpty();
            string uri = $"{ApiUrls.Albums}/{idAlbum}/images";
            string? token = await _tokenService.GetToken();
            UnathorizedException.ThrowIfTrue(string.IsNullOrWhiteSpace(token));
            RestResult restResult = await _rest.Send(uri, idImage, RestServices.Enums.RequestMethods.Delete, token: token!);
            if (restResult.HttpStatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            if (restResult.HttpStatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                ErrorResult? errorResult = JsonSerializer.Deserialize<ErrorResult>(restResult.Result);
                throw new Exception(errorResult!.Message);
            }
            if (restResult.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            }
            SuccessResult? result = JsonSerializer.Deserialize<SuccessResult>(restResult.Result) ?? throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            return result;
        }

        public async Task<AlbumsDto> Update(AlbumsDto item, string id)
        {
            string uri = $"{ApiUrls.Albums}/{id}";
            string? token = await _tokenService.GetToken();
            UnathorizedException.ThrowIfTrue(string.IsNullOrWhiteSpace(token));
            AlbumsInput input = _mapper.Map<AlbumsInput>(item);

            RestResult restResult = await _rest.Send(uri, input, RestServices.Enums.RequestMethods.Put, token: token!);
            if (restResult.HttpStatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            if (restResult.HttpStatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                ErrorResult? errorResult = JsonSerializer.Deserialize<ErrorResult>(restResult.Result);
                throw new Exception(errorResult!.Message);
            }
            if (restResult.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            }
            AlbumsResult? result = JsonSerializer.Deserialize<AlbumsResult>(restResult.Result) ?? throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            return _mapper.Map<AlbumsDto>(result);
        }
    }
    
}
