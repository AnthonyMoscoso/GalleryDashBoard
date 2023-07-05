using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Extensions
{
    public static class HttpStatusCodeExtensions
    {
        public static bool IsUnathorized(this HttpStatusCode httpStatusCode)
        {
            return httpStatusCode == HttpStatusCode.Unauthorized;
        }
    }
}
