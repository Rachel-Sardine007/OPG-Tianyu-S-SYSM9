using OPG_Tianyu_Shi_SYSM9_CookMaster.Managers;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Models;
using OPG_Tianyu_Shi_SYSM9_CookMaster.MVVM;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Service;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly UserManager _userManager;
        private string _username { get; set; }
        private string _password { get; set; }
        private string _confirmPassword {  get; set; } // re-enter password
        private CountryItem _selectedCountry { get; set; } 
        private string _error {  get; set; }

        public ObservableCollection<CountryItem> CountryList { get; } // get CountryList

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

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();

                if(_confirmPassword != _password)
                {
                    Error = $"Passwords do not match";
                }
                else
                {
                    Error = string.Empty;
                }
            }
        }

        public CountryItem SelectedCountry // SelectedCountry has two props: name & code
        {
            get => _selectedCountry;
            set
            {
                _selectedCountry = value;
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
            CountryList = new ObservableCollection<CountryItem>(CountryService.LoadCountryList());
            RegisterCommand = new RelayCommand(
                excute => CreateUser(),
                canExcute => canRegister());


        }

        // Control blank space
        private bool canRegister()
        {
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password)
            && !string.IsNullOrWhiteSpace(ConfirmPassword) && !string.IsNullOrWhiteSpace(SelectedCountry?.Name)) 
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
                if (_userManager.ValidatePassword(Password))
                {
                    _userManager.Register(Username, Password, SelectedCountry?.Name);
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
