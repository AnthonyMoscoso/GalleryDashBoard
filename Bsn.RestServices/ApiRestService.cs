using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsn.RestServices
{
    public class ApiRestService : BaseRestService, IRest
    {
        public ApiRestService(HttpClient httpClient) : base(httpClient)
        {
            HttpClient = httpClient;
        }
    }
}
