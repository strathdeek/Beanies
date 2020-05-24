using Beanies.Models;
using Beanies.Models.BackendResponses;
using Beanies.Services.Backend.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Beanies.Services.Backend
{
    class UserService : AbstractBackendService, IUserService
    {
        private ISessionService sessionService => DependencyService.Get<ISessionService>();
        private IConfigurationService configurationService => DependencyService.Get<IConfigurationService>();
        private string baseUrl => $"{configurationService.BackendBaseUrl}/users";
        public async Task<User> RegisterGuestAsync(string name)
        {
            Dictionary<string, string> body = new Dictionary<string, string>()
            {
                {"name",name }
            };

            string registerUrl = $"{baseUrl}/guest";
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
            var res = await GetAsync(baseUrl, token: sessionService.Token);
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
            string userUrl = $"{baseUrl}/{id}";
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

            string loginUrl = $"{baseUrl}/login";
            var res = await PostAsync(loginUrl,body);
            if (!res.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                var content = await res.Content.ReadAsStringAsync();
                var loginReponse = JsonConvert.DeserializeObject<LoginReponse>(content);
                sessionService.Self = loginReponse.user;
                sessionService.Token = loginReponse.token;
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

            string registerUrl = $"{baseUrl}/register";
            var res = await PostAsync(registerUrl, body);
            if (!res.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                var content = await res.Content.ReadAsStringAsync();
                var loginReponse = JsonConvert.DeserializeObject<LoginReponse>(content);
                sessionService.Self = loginReponse.user;
                sessionService.Token = loginReponse.token;
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

            string registerUrl = $"{baseUrl}/register/{id}";
            var res = await PostAsync(registerUrl, body);
            if (!res.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                var content = await res.Content.ReadAsStringAsync();
                var loginReponse = JsonConvert.DeserializeObject<LoginReponse>(content);
                sessionService.Self = loginReponse.user;
                sessionService.Token = loginReponse.token;
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

            var res = await PutAsync(baseUrl, body, token: sessionService.Token);
            if (!res.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                var content = await res.Content.ReadAsStringAsync();
                var userResponse = JsonConvert.DeserializeObject<UserResponse>(content);
                sessionService.Self = userResponse.user;
                return true;
            }
        }

        public async Task<bool> UpdateGuestAsync(string id, string name = "")
        {
            string userUrl = $"{baseUrl}/{id}";
            Dictionary<string, string> body = new Dictionary<string, string>();
            body.Add("name", name);

            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            var res = await PutAsync(userUrl, body, token: sessionService.Token);
            if (!res.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                var content = await res.Content.ReadAsStringAsync();
                var userResponse = JsonConvert.DeserializeObject<UserResponse>(content);
                sessionService.Self = userResponse.user;
                return true;
            }
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            string userUrl = $"{baseUrl}/{id}";

            var res = await DeleteAsync(userUrl, token: sessionService.Token);
            return res.IsSuccessStatusCode;
        }
    }
}
