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
        private string tokenExpirationKey = nameof(TokenExpiration);
        private bool hasToken => !string.IsNullOrEmpty(Token);
        private bool tokenValid => (TokenExpiration != null) && TokenExpiration > DateTime.Now;

        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
        public User Self { get; set; }

        public bool HasActiveSession()
        {
            if (!hasToken || TokenExpiration==null)
            {
                FetchSessionData();
            }

            if (hasToken && tokenValid)
            {
                if (Self == null)
                    UpdateCurrentUserAsync();
                return true;
            }
            return false;
        }

        private async Task UpdateCurrentUserAsync()
        {
            var userService = DependencyService.Resolve<IUserBackendService>();

            var currentUser = await userService.GetSelfAsync();
            Self = currentUser;
        }

        private void FetchSessionData()
        {
            string token = string.Empty;
            string tokenExpiration = string.Empty;

            if (App.Current.Properties.ContainsKey(tokenKey))
                token = App.Current.Properties[tokenKey] as string;
            if (App.Current.Properties.ContainsKey(tokenExpirationKey))
                tokenExpiration = App.Current.Properties[tokenExpirationKey] as string;

            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(tokenExpiration))
            {
                Token = token;
                TokenExpiration = DateTime.Parse(tokenExpiration);
            }
        }

        public void SaveSessionData()
        {
            if (!App.Current.Properties.ContainsKey(tokenKey))
                App.Current.Properties.Add(tokenKey, Token);
            else
                App.Current.Properties[tokenKey] = Token;

            if (!App.Current.Properties.ContainsKey(tokenKey))
                App.Current.Properties.Add(tokenExpirationKey, TokenExpiration.ToShortDateString());
            else
                App.Current.Properties[tokenExpirationKey] = TokenExpiration.ToShortDateString();

            App.Current.SavePropertiesAsync();
        }
    }
}
