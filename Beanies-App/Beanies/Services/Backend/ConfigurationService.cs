using System;
using System.Collections.Generic;
using System.Text;

namespace Beanies.Services.Backend
{
    class ConfigurationService : IConfigurationService
    {
        public string BackendBaseUrl => "http://www.strathdeek.com:5000";
    }
}
