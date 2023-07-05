using Model.Entity.ImageFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity.Albums
{
    public class AlbumsResult
    {
        public AlbumsResult()
        {
            Images = new HashSet<ImageFilesResult>();
        }
        public string IdAlbum { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTimeOffset Created { get; set; }
        public string UrlCover { get; set; } = string.Empty;
        public int TotalItems { get; set; } = 0;
        public ICollection<ImageFilesResult> Images { get; set; }
    }
}
