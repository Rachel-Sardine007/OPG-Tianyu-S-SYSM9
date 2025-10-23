using OPG_Tianyu_Shi_SYSM9_CookMaster.Managers;
using OPG_Tianyu_Shi_SYSM9_CookMaster.MVVM;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.ViewModels
{
    public class RegisterViewModel: ViewModelBase
    {   
        // Property
        private UserManager _userManager;
        private string _username { get; set; }
        private string _password1 { get; set; }
        private string _password2 {  get; set; } // re-enter password
        private string _country { get; set; }
        private string _error {  get; set; }

        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged();

                    // Call FindUser method to check if username already exists ?? 
                    if (_userManager.FindUser(Username))
                    {
                        Error = "Username already exists";
                 
                    }
                    else
                    {
                        Error = string.Empty;
                    }

                    // Call property when RegisterCommand runs
                    CommandManager.InvalidateRequerySuggested();

                }
            }
        }

        public string Password1
        {
            get => _password1;
            set
            {
                _password1 = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string Password2
        {
            get => _password2;
            set
            {
                _password2 = value;
                OnPropertyChanged();

                if(_password2 != _password1)
                {
                    Error = $"Passwords don't match, re-enter password again";
                }
                else
                {
                    Error = string.Empty;
                }
            }
        }

        public string Country
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string Error
        {
            get => _error;
            set
            {
                if (_error != value)
                {
                    _error = value;

                    OnPropertyChanged();
                }
            }
        }

        // Constructor
        public RegisterViewModel( UserManager userManager)
        {
            _userManager = userManager;
            RegisterCommand = new RelayCommand(
                excute => CreateUser(),
                canExcute => canRegister());
        }

        // Control blank space
        private bool canRegister()
        {
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password1)
            && !string.IsNullOrWhiteSpace(Country))
            {
                return true;
            }
                return false; 
        }
            
       

        // ?? Call usermanager methods to check if username already exists and validate password 
        // Event Dialog 
        private void CreateUser()
        {
            if (!_userManager.FindUser(Username))
            {
                if (_userManager.ValidatePassword(Password1))
                {
                    _userManager.Register(Username, Password1, Country);
                    OnRegisterSuccess?.Invoke(this, System.EventArgs.Empty);
                    MessageBox.Show("Register successfully!");
                }
                else
                {
                    Error = "Password must be 8 character long " +
                        "and includes 1 number and 1 special character!";
                }
            }
                
        }

        public ICommand RegisterCommand { get; }
        public event System.EventHandler OnRegisterSuccess;
    }
}
