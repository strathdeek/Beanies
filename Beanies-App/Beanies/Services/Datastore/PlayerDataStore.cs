using Beanies.Models;
using Beanies.Services.Backend.Interfaces;
using Beanies.Services.LocalDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Beanies.Services.Datastore
{
    class PlayerDataStore : IDataStore<User>
    {
        IUserBackendService UserBackendService => DependencyService.Resolve<IUserBackendService>();
        UserDatabase userDatabase => DependencyService.Resolve<UserDatabase>();

        public async Task<bool> AddAsync(User item)
        {
            var newUser = await UserBackendService.RegisterGuestAsync(item.Name);
            if (newUser!=null)
            {
                await userDatabase.SaveUserAsync(newUser);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var deleted = await UserBackendService.DeleteUserAsync(id);
            var userToDelete = await userDatabase.GetUserByRemoteId(id);
            return deleted && (await userDatabase.DeleteUserAsync(userToDelete) != 0);
        }

        public async Task<IEnumerable<User>> GetAllAsync(bool forceRefresh = false)
        {
            var users = await userDatabase.GetUsersAsync();
            return users;
        }

        public async Task<User> GetAsync(string id)
        {
            var user = await userDatabase.GetUserByRemoteId(id);
            if (user == null)
                user = await UserBackendService.GetUserAsync(id);
            return user;
        }

        public async Task<bool> UpdateAsync(User item)
        {
            var updatedPlayerSuccessfully = await UserBackendService.UpdateGuestAsync(item.RemoteId, item.Name);
            await userDatabase.SaveUserAsync(item);
            return updatedPlayerSuccessfully;
        }
    }
}
