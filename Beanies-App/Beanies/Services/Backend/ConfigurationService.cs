using System;
using System.Collections.Generic;
using System.Text;

namespace Beanies.Services.Backend
{
    class ConfigurationService : IConfigurationService
    {
        public string BackendBaseUrl => "https://www.strathdeek.com/api";
    }
}
