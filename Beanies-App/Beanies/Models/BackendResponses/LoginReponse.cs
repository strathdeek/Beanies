using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beanies.Models.BackendResponses
{
    class LoginReponse
    {
        public User user { get; set; }
        public bool success { get; set; }
        public string token { get; set; }
    }
}
