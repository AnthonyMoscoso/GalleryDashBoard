using Bsn.Utilities.Configuration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsn.Utilities.Configuration
{
    public class Keys : IKeys
    {
        private IConfigurationKeys _configurationKeys;
        public Keys(IConfigurationKeys configurationKeys) 
        {
            _configurationKeys = configurationKeys;
        }
        public IConfigurationKeys Config => _configurationKeys ;
    }
}
