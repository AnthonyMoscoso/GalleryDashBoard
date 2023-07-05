using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bsn.RestServices.Models
{
    public class RestResult
    {
        public string Result { get; set; } = string.Empty;
        public HttpStatusCode HttpStatusCode { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
