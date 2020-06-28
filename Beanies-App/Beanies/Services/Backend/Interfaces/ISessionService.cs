using Beanies.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beanies.Services.Backend.Interfaces
{
    interface ISessionService
    {
        string Token { get; set; }
        string Self { get; set; }

        bool HasActiveSession();
        void SaveSessionData();
    }
}
