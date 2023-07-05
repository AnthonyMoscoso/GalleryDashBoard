using Model.Dto.Table;
using Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity.Albums;

namespace Bsn.DataServices.Interfaces
{
    public interface IAlbumServices
    {
        Task<AlbumsDto> GetDetail(string id);
        Task<DataTableInfo<AlbumsDto>> DataTable(TableModel tableModel, string? search = null,bool? all = false);
        Task<AlbumsDto> Insert(AlbumsDto item);

        Task<SuccessResult> AddImage(string idAlbum, AlbumImageDto albumImageDto);
        Task<SuccessResult> RemoveImage(string idAlbum, string idImage);
        Task<AlbumsDto> Update(AlbumsDto item, string id);
        Task<SuccessResult> Delete(string id);
    }
}
