using System;
using System.Collections.Generic;
using System.Text;

namespace Beanies.Models.BackendResponses
{
    class UserResponse
    {
        public User user { get; set; }
        public string msg { get; set; }
        public bool success { get; set; }
    }
}
