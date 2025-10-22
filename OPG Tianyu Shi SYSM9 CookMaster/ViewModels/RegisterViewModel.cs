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
        private string _password { get; set; }
        private string _country { get; set; }
        private string _error {  get; set; }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
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
                _error = value;
                OnPropertyChanged();
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
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password)
            && !string.IsNullOrWhiteSpace(Country))
            {
                return true;
            }
            Error = "You must choose a country";
            return false;
        }
            
       

        // Call usermanager methods to check if username already exists and validate password
        // Event Dialog
        public void CreateUser()
        {
            if (_userManager.FindUser(Username))
            {
                if (_userManager.ValidatePassword(Password))
                {
                    _userManager.Register(Username, Password, Country);
                    OnRegisterSuccess?.Invoke(this, System.EventArgs.Empty);
                }else
                {
                    Error = "Password must be 8 character long and includes 1 number and 1 special character!";
                }
            }
            else
            {
                Error = "Username already exists";
            }
                
        }

        public ICommand RegisterCommand { get; }
        public event System.EventHandler OnRegisterSuccess;

        private void Register()
        {
            // Save reference to the oldMain
            var oldMain = Application.Current.MainWindow;

            // Close MainWindow without att end the program
            if (oldMain != null)
                oldMain.Close();

            // Show register window
            var register = new RegisterWindow();
            var result = register.ShowDialog();

            
        }
    }
}
