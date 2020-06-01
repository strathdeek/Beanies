using Beanies.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beanies.Services.Backend.Interfaces
{
    interface IUserService
    {
        Task<bool> LoginAsync(string email, string password);

        Task<bool> RegisterAsync(string email, string password, string name);

        Task<bool> RegisterFromGuestAsync(string email, string password, string name, string id);

        Task<User> RegisterGuestAsync(string name);

        Task<User> GetSelfAsync();

        Task<User> GetUserAsync(string id);

        Task<bool> UpdateSelfAsync(string email, string password, string name);

        Task<bool> UpdateGuestAsync(string id, string name);

        Task<bool> DeleteUserAsync(string id);
    }
}
