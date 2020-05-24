using System;
using System.Collections.Generic;
using System.Text;

namespace Beanies.Models.BackendResponses
{
    class GenericError
    {
        public bool success { get; set; }
        public string[] errors { get; set; }
    }
}
