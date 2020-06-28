using Beanies.Models;
using Beanies.Models.BackendResponses;
using Beanies.Services.Backend.Interfaces;
using Beanies.Services.LocalDatabase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Beanies.Services.Backend
{
    class UserBackendService : AbstractBackendService, IUserBackendService
    {
        public IDataStore<User> PlayerDataStore => DependencyService.Get<IDataStore<User>>();
        private UserDatabase userDatabase => DependencyService.Resolve<UserDatabase>();

        private string userUrl => $"{baseUrl}/users";
        public async Task<User> RegisterGuestAsync(string name)
        {
            Dictionary<string, string> body = new Dictionary<string, string>()
            {
                {"name",name }
            };

            string registerUrl = $"{userUrl}/guest";
            var res = await PostAsync(registerUrl, body);
            if (!res.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await res.Content.ReadAsStringAsync();
                var loginReponse = JsonConvert.DeserializeObject<LoginReponse>(content);
                return loginReponse.user;
            }
        }

        public async Task<User> GetSelfAsync()
        {
            var res = await GetAsync(userUrl, token: sessionService.Token);
            if (res.IsSuccessStatusCode)
            {
                var content = await res.Content.ReadAsStringAsync();
                var userResponse = JsonConvert.DeserializeObject<UserResponse>(content);
                return userResponse.user;
            }
            else
            {
                return null;
            }

        }

        public async Task<User> GetUserAsync(string id)
        {
            string userUrl = $"{this.userUrl}/{id}";
            var res = await GetAsync(userUrl, token: sessionService.Token);
            if (res.IsSuccessStatusCode)
            {
                var content = await res.Content.ReadAsStringAsync();
                var userResponse = JsonConvert.DeserializeObject<UserResponse>(content);
                return userResponse.user;
            }
            else
            {
                return null;
            }

        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            Dictionary<string, string> body = new Dictionary<string, string>()
            {
                { "email", email },
                {"password",password }
            };

            string loginUrl = $"{userUrl}/login";
            var res = await PostAsync(loginUrl,body);
            if (!res.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                var content = await res.Content.ReadAsStringAsync();
                var loginReponse = JsonConvert.DeserializeObject<LoginReponse>(content);
                SaveSessionData(loginReponse);
                return true;
            }

        }

        public async Task<bool> RegisterAsync(string email, string password, string name)
        {
            Dictionary<string, string> body = new Dictionary<string, string>()
            {
                { "email", email },
                {"password",password },
                {"name",name }
            };

            string registerUrl = $"{userUrl}/register";
            var res = await PostAsync(registerUrl, body);
            if (!res.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                var content = await res.Content.ReadAsStringAsync();
                var loginReponse = JsonConvert.DeserializeObject<LoginReponse>(content);
                SaveSessionData(loginReponse);
                return true;
            }
        }

        public async Task<bool> RegisterFromGuestAsync(string email, string password, string name, string id)
        {
            Dictionary<string, string> body = new Dictionary<string, string>()
            {
                { "email", email },
                {"password",password },
                {"name",name }
            };

            string registerUrl = $"{userUrl}/register/{id}";
            var res = await PostAsync(registerUrl, body);
            if (!res.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                var content = await res.Content.ReadAsStringAsync();
                var loginReponse = JsonConvert.DeserializeObject<LoginReponse>(content);
                SaveSessionData(loginReponse);
                return true;
            }
        }

        public async Task<bool> UpdateSelfAsync(string email = "", string password = "", string name = "")
        {
            Dictionary<string, string> body = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(email))
            {
                body.Add("email", email);
            }
            if (!string.IsNullOrEmpty(name))
            {
                body.Add("name", name);
            }
            if (!string.IsNullOrEmpty(password))
            {
                body.Add("password", password);
            }

            var res = await PutAsync(userUrl, body, token: sessionService.Token);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateGuestAsync(string id, string name = "")
        {
            string userUrl = $"{this.userUrl}/{id}";
            Dictionary<string, string> body = new Dictionary<string, string>();
            body.Add("name", name);

            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            var res = await PutAsync(userUrl, body, token: sessionService.Token);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            string userUrl = $"{this.userUrl}/{id}";

            var res = await DeleteAsync(userUrl, token: sessionService.Token);
            return res.IsSuccessStatusCode;
        }

        private void SaveSessionData(LoginReponse loginReponse)
        {
            sessionService.Self = loginReponse.user.RemoteId;
            sessionService.Token = loginReponse.token;
            sessionService.SaveSessionData();

            userDatabase.SaveUserAsync(loginReponse.user);
        }
    }
}
