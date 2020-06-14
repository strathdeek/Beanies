using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Beanies.Services.Backend.Interfaces
{
    abstract class AbstractBackendService
    {
        protected ISessionService sessionService => DependencyService.Get<ISessionService>();
        protected IConfigurationService configurationService => DependencyService.Get<IConfigurationService>();
        protected string baseUrl => configurationService.BackendBaseUrl;
        internal async Task<HttpResponseMessage> PostAsync(string url, object requestBody, string token = "")
        {
            string body = JsonConvert.SerializeObject(requestBody);
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                return await client.PostAsync(url,content);
            }
        }

        internal async Task<HttpResponseMessage> PutAsync(string url, object requestBody, string token = "")
        {
            string body = JsonConvert.SerializeObject(requestBody);
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                return await client.PutAsync(url, content);
            }
        }

        internal async Task<HttpResponseMessage> GetAsync(string url, string token = "")
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return await client.GetAsync(url);
            }
        }

        internal async Task<HttpResponseMessage> DeleteAsync(string url, string token = "")
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return await client.DeleteAsync(url);
            }
        }
    }
}
