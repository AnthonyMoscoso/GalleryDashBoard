using AutoMapper;
using Bsn.DataServices.Interfaces;
using Bsn.RestServices.Models;
using Bsn.RestServices;
using Bsn.Utilities.Constants;
using Bsn.Utilities.Token.Interfaces;
using Core.Utilities.Exceptions;
using Model.Dto;
using Model.Dto.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Model.Entity.ImageFiles;
using Core.Utilities.Ensures;

namespace Bsn.DataServices
{
    public class ImageFileServicies : IImageFileServicies
    {
        private readonly IRest _rest;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public ImageFileServicies(IRest rest, ITokenService tokenService, IMapper mapper)
        {
            _rest = rest;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        public async Task<DataTableInfo<ImageFileDto>> DataTable(TableModel tableModel, string? search = null, string? idAlbum = null, bool? all = false)
        {
            string uri = $"{ApiUrls.Images}?search={search}&take={tableModel.Take}&skip={tableModel.Skip}&orderBy={tableModel.Sorted}&isAsc={tableModel.IsAsc}&all={all}";
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
            DataTableInfo<ImageFilesResult>? rolResult = JsonSerializer.Deserialize<DataTableInfo<ImageFilesResult>>(restResult.Result) ?? throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            DataTableInfo<ImageFileDto> dataTableInfo = new()
            {
                TotalItems = rolResult.TotalItems,
                Items = _mapper.Map<IEnumerable<ImageFileDto>>(rolResult.Items)
            };

            return dataTableInfo;
        }

        public async Task<SuccessResult> Delete(string id)
        {
            Ensure.That(id,nameof(id)).NotNullOrEmpty();
            string uri = $"{ApiUrls.Images}";
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

        public async Task<ImageFileDto> GetDetail(string id)
        {
            string uri = $"{ApiUrls.Images}/{id}";
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
            ImageFilesResult? datas = JsonSerializer.Deserialize<ImageFilesResult>(restResult.Result) ?? throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            ImageFileDto dto = _mapper.Map<ImageFileDto>(datas);
            return dto;
        }

        public async Task<ImageFileDto> Insert(ImageFileDto item)
        {
           
            string uri = $"{ApiUrls.Images}";
            string? token = await _tokenService.GetToken();
            UnathorizedException.ThrowIfTrue(string.IsNullOrWhiteSpace(token));
            ImageFilesInput input = _mapper.Map<ImageFilesInput>(item);       
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
            ImageFilesResult? result = JsonSerializer.Deserialize<ImageFilesResult>(restResult.Result) ?? throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            return _mapper.Map<ImageFileDto>(result);
        }

        public async Task<ImageFileDto> Update(ImageFileDto item,string id)
        {

            string uri = $"{ApiUrls.Images}/{id}";
            string? token = await _tokenService.GetToken();
            UnathorizedException.ThrowIfTrue(string.IsNullOrWhiteSpace(token));
            ImageFilesInput input = _mapper.Map<ImageFilesInput>(item);
          
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
            ImageFilesResult? result = JsonSerializer.Deserialize<ImageFilesResult>(restResult.Result) ?? throw new Exception(ErrorMessages.INTERNAL_SERVER_ERROR);
            return _mapper.Map<ImageFileDto>(result);
        }
    }
}
