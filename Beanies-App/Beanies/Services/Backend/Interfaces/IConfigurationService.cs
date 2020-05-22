using System;
using System.Collections.Generic;
using System.Text;

namespace Beanies.Services
{
    interface IConfigurationService
    {
         string BackendUrl { get; }
    }
}
