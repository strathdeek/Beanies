using Beanies.Models;
using Beanies.Services.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beanies.Services.Backend
{
    class SessionService : ISessionService
    {
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
        public User Self { get; set; }
    }
}
