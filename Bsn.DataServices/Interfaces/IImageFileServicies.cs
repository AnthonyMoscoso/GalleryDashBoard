using Model.Dto;
using Model.Dto.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsn.DataServices.Interfaces
{
    public interface IImageFileServicies
    {
         Task<ImageFileDto> GetDetail(string id);
         Task<DataTableInfo<ImageFileDto>> DataTable(TableModel tableModel, string? search = null, string? idAlbum = null, bool? all = false);
         Task<ImageFileDto> Insert(ImageFileDto item);
         Task<ImageFileDto> Update(ImageFileDto item, string id);
         Task<SuccessResult> Delete(string id);
    }
}
