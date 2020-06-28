using Beanies.Models;
using Beanies.Services.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Beanies.Services.Backend
{
    class SessionService : ISessionService
    {
        private string tokenKey = nameof(Token);
        private string selfKey = nameof(Self);

        private bool hasToken => !string.IsNullOrEmpty(Token);
        private bool hasSelf => !string.IsNullOrEmpty(Self);

        public string Token { get; set; }
        public string Self { get; set; }

        public bool HasActiveSession()
        {
            if (!hasToken)
            {
                FetchSessionData();
            }
            return hasToken;
        }

        private void FetchSessionData()
        {
            string token = string.Empty;
            string self = string.Empty;

            if (App.Current.Properties.ContainsKey(tokenKey))
                token = App.Current.Properties[tokenKey] as string;

            if (App.Current.Properties.ContainsKey(selfKey))
                self = App.Current.Properties[selfKey] as string;

            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(self))
            {
                Token = token;
                Self = self;
            }
        }

        public void SaveSessionData()
        {
            if (!App.Current.Properties.ContainsKey(tokenKey))
                App.Current.Properties.Add(tokenKey, Token);
            else
                App.Current.Properties[tokenKey] = Token;

            if (!App.Current.Properties.ContainsKey(selfKey))
                App.Current.Properties.Add(selfKey, Self);
            else
                App.Current.Properties[selfKey] = Self;

            App.Current.SavePropertiesAsync();
        }
    }
}
