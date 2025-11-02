using OPG_Tianyu_Shi_SYSM9_CookMaster.Managers;
using OPG_Tianyu_Shi_SYSM9_CookMaster.MVVM;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Views;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        // Property
        private readonly UserManager _userManager;
        private string _username;
        private string _password;
        private string _error;

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
        public string Error
        {
            get => _error;
            set
            {
                _error = value;
                OnPropertyChanged();
            }
        }

        // SetUp Login Command
        public ICommand LoginCommand { get; }

        public event System.EventHandler OnLoginSuccess;

        public MainViewModel(UserManager userManager)
        {
            _userManager = userManager;
            LoginCommand = new RelayCommand(
                execute => Login(),
                canExecute => Canlogin());
        }

        private bool Canlogin() => 
            !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
     
        private void Login()
        {
            if (_userManager.Login(Username, Password))
            {
                OnLoginSuccess?.Invoke(this, System.EventArgs.Empty);
            }
            else
            {
                Error = "Incorrect username or password.";
            }

        }


    }
}
