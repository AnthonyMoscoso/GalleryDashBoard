using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    public class ImageFileDto
    {
        public string IdImage { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Base64 { get; set; }
        public string Url { get; set; } = string.Empty;
        public DateTimeOffset? Updated { get; set; }
    }
}
