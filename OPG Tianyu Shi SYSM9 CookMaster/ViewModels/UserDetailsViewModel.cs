using OPG_Tianyu_Shi_SYSM9_CookMaster.Managers;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Models;
using OPG_Tianyu_Shi_SYSM9_CookMaster.MVVM;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Service;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.ViewModels
{
    public class UserDetailsViewModel: ViewModelBase
    {
        // Get current user info
        private readonly UserManager _userManager;

        // Editable fields
        private string _username;
        private CountryItem _selectedCountry;
        private string _currentPassword;
        private string _newPassword;
        private string _confirmPassword;
        private string _error;

        public string Username
        {
            get => _username;
            set { 
                _username = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public CountryItem SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                _selectedCountry = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string CurrentPassword
        {
            get { return _currentPassword; }
            set
            {
                _currentPassword = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
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
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string Error
        {
            get => _error;
            set {  _error = value; 
                OnPropertyChanged(); }
        }

        // Load countrylist
        public ObservableCollection<CountryItem> CountryList { get; }

        // Commands
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand ChangePasswordCommand {  get; }


        // Constructor
        public UserDetailsViewModel(UserManager userManager)
        {
            _userManager = userManager;
            CountryList = new ObservableCollection<CountryItem>(CountryService.LoadCountryList());
            var user = _userManager.CurrentUser;
            
            // Display current user values
            Username = user.Username;
            SelectedCountry = user.Country;

            CurrentPassword = user.Password;

            SaveCommand = new RelayCommand(_ => SaveChanges(), _ => CanSave());
            CancelCommand = new RelayCommand(_ => Cancel());
            ChangePasswordCommand = new RelayCommand(_ => ChangePassword(), _ => CanChangePassword());
        }

        public event System.EventHandler OnPasswordChanged;
        private bool CanChangePassword()
        {
            if (!string.IsNullOrWhiteSpace(CurrentPassword) && 
                !string.IsNullOrWhiteSpace(NewPassword) && !string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                return true;
            }
            return false;
        }

        private void ChangePassword() 
        {
            var user = _userManager.CurrentUser;
            if (user.Password == CurrentPassword)
            {
                if (NewPassword.Length >= 5)
                {
                    if (NewPassword == ConfirmPassword)
                    {
                        Error = string.Empty;
                        // call change password method 
                        _userManager.ChangePassword(Username, NewPassword);
                        OnPasswordChanged?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        Error = "Passwords do not match!";
                    }
                }
                else
                {
                    Error = "Password must be more than 5 character long.";
                }
            }
            else
            {
                Error = "Current Password incorrect!";
            }
        }
        

        private bool CanSave()
        {
            if ( !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(SelectedCountry.Name))
            {
                return true;
            }
            return false;
        }

        public event System.EventHandler OnSaveSuccess;

        // ?? save country information 
        private void SaveChanges()
        {
            var user = _userManager.CurrentUser;
            if (user.Username == Username) // if username stays the same but country different 
            {
                Error = string.Empty;
                // Call userManager updateUser method
                _userManager.UpdateUser(Username, SelectedCountry);

                // Trigger event 
                OnSaveSuccess?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                if (!_userManager.FindUser(Username))
                {
                    if (Username.Length >= 3)
                    {
                        Error = string.Empty;
                        // Call userManager updateUser method
                        _userManager.UpdateUser(Username, SelectedCountry);

                        // Trigger event 
                        OnSaveSuccess?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        Error = "Username must be over 3 character long.";
                    }
                }
                else
                {
                    Error = "Username already exists!";
                }
            }
            
        }

        public event System.EventHandler OnCancelRequested;
        private void Cancel()
        {
            // Event trigger 
            OnCancelRequested?.Invoke(this, EventArgs.Empty);
            
        }
    }
}
