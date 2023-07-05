using AutoMapper;
using Model.Dto;
using Model.Entity.Albums;
using Model.Entity.ImageFiles;

namespace Bsn.MapperService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ImageFileDto, ImageFilesInput>().ReverseMap();
            CreateMap<ImageFileDto,ImageFilesResult>().ReverseMap();

            CreateMap<AlbumsDto,AlbumsInput>().ReverseMap();
            CreateMap<AlbumsDto,AlbumsResult>().ReverseMap();

            CreateMap<AlbumImageDto,AlbumsImageInput>().ReverseMap();
        }
    }
}
