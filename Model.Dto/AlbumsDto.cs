using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    public class AlbumsDto
    {
        public AlbumsDto()
        {
            Images = new HashSet<ImageFileDto>();
        }
        public string IdAlbum { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTimeOffset Created { get; set; }
        public string UrlCover { get; set; } = string.Empty;
        public int TotalItems { get; set; } = 0;
        public ICollection<ImageFileDto> Images { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            AlbumsDto otherObj = (AlbumsDto)obj;

            return
                   Name.Equals(otherObj.Name) &&
                       Images.SequenceEqual(otherObj.Images);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (Name != null ? Name.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
