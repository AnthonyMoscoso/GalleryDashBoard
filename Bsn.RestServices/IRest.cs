using Bsn.RestServices.Enums;
using Bsn.RestServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsn.RestServices
{
    public interface IRest
    {
        HttpClient HttpClient { get; set; }
        Task<RestResult> Get(string url, string token = "", IDictionary<string, string>? headers = null);
        Task<RestResult> Send<T>(string url, T objectRequest, RequestMethods requestMethod = RequestMethods.Post  ,string token = "", IDictionary<string, string>? headers = null);
      
    }
}
