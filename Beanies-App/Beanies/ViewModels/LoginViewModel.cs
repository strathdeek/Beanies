using Beanies.Services.Backend;
using Beanies.Services.Backend.Interfaces;
using Beanies.Styles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Beanies.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        IUserBackendService userService => DependencyService.Resolve<IUserBackendService>();

        public LoginViewModel()
        {
        }
        private string password;
        public string Password
        {
            get { return password; }
            set 
            { 
                password = value; 
                OnPropertyChanged(nameof(Password)); 
            }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set 
            { 
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string LoginIcon => IconFont.ArrowRight;

        public async Task<bool> LoginAsync()
        {
            return await userService.LoginAsync(Email, Password);
        }
    }


}
