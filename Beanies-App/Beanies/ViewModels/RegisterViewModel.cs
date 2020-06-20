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
            var nameValid = !string.IsNullOrEmpty(Name);
            var emailEntered = !string.IsNullOrEmpty(Email);
            var emailValid = new EmailAddressAttribute().IsValid(Email);
            var passwordEntered = !string.IsNullOrEmpty(Password);
            var password2Entered = !string.IsNullOrEmpty(password2);
            var passwordsMatch = password == password2;

            ShowErrorMessage = !(nameValid &&
                               emailEntered &&
                               emailValid &&
                               passwordEntered &&
                               password2Entered &&
                               passwordsMatch);

            return !ShowErrorMessage;
        }
    }
}
