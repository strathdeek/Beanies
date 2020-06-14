using Beanies.Services.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Beanies.ViewModels
{
    class RegisterViewModel : BaseViewModel
    {
        private bool showErrorMessage;

        public bool ShowErrorMessage
        {
            get { return showErrorMessage; }
            set { showErrorMessage = value; OnPropertyChanged(nameof(ShowErrorMessage)); }
        }

        IUserBackendService userService => DependencyService.Resolve<IUserBackendService>();

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value;
                OnPropertyChanged(nameof(Name)); }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(nameof(Email)); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(nameof(Password)); }
        }

        private string password2;

        public string Password2
        {
            get { return password2; }
            set { password2 = value; OnPropertyChanged(nameof(Password2)); }
        }

        public async Task<bool> Register()
        {
            if (ValidateFields())
            {
                return await userService.RegisterAsync(Email, Password, Name);
            }
            return false;
        }

        private bool ValidateFields()
        {
            ShowErrorMessage = (!string.IsNullOrEmpty(Name) &&
                    !string.IsNullOrEmpty(Email) &&
                    new EmailAddressAttribute().IsValid(Email) &&
                    !string.IsNullOrEmpty(Password) &&
                    Password.Equals(Password2));
            return !ShowErrorMessage;
        }
    }
}
