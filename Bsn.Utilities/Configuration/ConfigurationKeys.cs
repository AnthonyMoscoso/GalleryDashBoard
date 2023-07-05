using Bsn.Utilities.Configuration.Interfaces;
using Bsn.Utilities.Constants;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsn.Utilities.Configuration
{
    public class ConfigurationKeys : IConfigurationKeys
    {
        private readonly IConfiguration _configuration;
        public ConfigurationKeys(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string? ApiUri => _configuration[Constant.ApiUrl];
    }
}
