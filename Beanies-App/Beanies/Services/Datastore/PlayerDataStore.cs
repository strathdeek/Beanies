using Beanies.Models;
using Beanies.Services.Backend.Interfaces;
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
        ISessionService sessionService => DependencyService.Resolve<ISessionService>();

        List<User> Users = new List<User>();
        public async Task<bool> AddAsync(User item)
        {
            var newUser = await UserBackendService.RegisterGuestAsync(item.Name);
            if (newUser!=null)
            {
                Users.Add(newUser);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var deleted = await UserBackendService.DeleteUserAsync(id);
            var userToDelete = Users.FirstOrDefault(x => x.Id == id);
            return Users.Remove(userToDelete) && deleted;
        }

        public async Task<IEnumerable<User>> GetAllAsync(bool forceRefresh = false)
        {
            return Users;
        }

        public async Task<User> GetAsync(string id)
        {
            var user = Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
                user = await UserBackendService.GetUserAsync(id);
            return user;
        }

        public async Task<bool> UpdateAsync(User item)
        {
            var updatedPlayerSuccessfully = await UserBackendService.UpdateGuestAsync(item.Id, item.Name);
            var indexToUpdate = Users.IndexOf(Users.FirstOrDefault(x=>x.Id==item.Id));
            if (updatedPlayerSuccessfully && indexToUpdate>=0)
            {
                Users[indexToUpdate] = item;
                return true;
            }
            return false;
        }
    }
}
