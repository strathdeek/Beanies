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

        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
        public User Self { get; set; }

        public bool HasActiveSession()
        {
            bool hasToken = !string.IsNullOrEmpty(Token);
            bool tokenValid = (TokenExpiration!=null) && TokenExpiration > DateTime.Now;

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
            Token = (string)Application.Current.Properties[tokenKey];
            TokenExpiration = (DateTime)Application.Current.Properties[tokenExpirationKey];
        }

        private void SaveSessionData()
        {
            Application.Current.Properties[tokenKey] = Token;
            Application.Current.Properties[tokenExpirationKey] = TokenExpiration;
        }
    }
}
