using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Beanies.ViewModels
{
    class LoginViewModel : BaseViewModel
    {

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

    }


}
