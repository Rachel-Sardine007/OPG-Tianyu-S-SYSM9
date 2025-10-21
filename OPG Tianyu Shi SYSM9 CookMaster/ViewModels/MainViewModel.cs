using OPG_Tianyu_Shi_SYSM9_CookMaster.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.ViewModels
{
    public class MainViewModel: INotifyPropertyChanged
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

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
