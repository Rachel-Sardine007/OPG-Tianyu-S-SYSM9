using OPG_Tianyu_Shi_SYSM9_CookMaster.Managers;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Models;
using OPG_Tianyu_Shi_SYSM9_CookMaster.MVVM;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Service;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
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
        private readonly RecipeManager _recipeManager;
        public User CurrentUser => _userManager.CurrentUser;

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
                }
        }

        public CountryItem SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                _selectedCountry = value;
                    OnPropertyChanged();
            }
        }

        public string CurrentPassword
        {
            get => _currentPassword;
            set
            { 
                    _currentPassword = value;
                    OnPropertyChanged(); 
            }
        }

        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged();
                // CommandManager.InvalidateRequerySuggested();
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
                // CommandManager.InvalidateRequerySuggested();
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
        public UserDetailsViewModel(UserManager userManager, RecipeManager recipeManager)
        {
            _userManager = userManager;
            _recipeManager = recipeManager;
            CountryList = new ObservableCollection<CountryItem>(CountryService.LoadCountryList());

            // Display current user info
            Username = CurrentUser.Username;
            SelectedCountry = CurrentUser.Country;
         
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
            if (CurrentUser.Password != CurrentPassword)
            {
                Error = "Current Password incorrect!";
                return;
            }
            if (CurrentPassword == NewPassword) {
                Error = "New password cannot be the same!";
                return;
            }
            if (NewPassword.Length < 5)
            {
                Error = "Password must be more than 5 character long.";
                return;
            }
            if (NewPassword != ConfirmPassword)
            {
                Error = "Passwords do not match!";
                return;
            }

            CurrentUser.Password = NewPassword;
            Error = string.Empty;
            OnPasswordChanged?.Invoke(this, EventArgs.Empty);
        }
        

        private bool CanSave()
        {
            if ( !string.IsNullOrWhiteSpace(Username) && SelectedCountry!=null && !string.IsNullOrWhiteSpace(SelectedCountry.Name))
            {
                return true;
            }
            return false;
        }

        public event System.EventHandler OnSaveSuccess;

        // ?? save country information 
        private void SaveChanges()
        {
            
            if (CurrentUser.Username != Username) 
            {
                if (_userManager.FindUser(Username))
                {
                    Error = "Username already exists!";
                    return;
                }
   
                if (Username.Length < 3)
                {
                    Error = "Username must be over 3 character long.";
                    return;
                }

                string oldUsername = CurrentUser.Username;
                string newUsername = Username;
                CurrentUser.Username = newUsername;
                _recipeManager.UpdateCreatedBy(oldUsername, newUsername);
            }
            CurrentUser.Country = SelectedCountry;
            Error = string.Empty;
            // Trigger event 
            OnSaveSuccess?.Invoke(this, EventArgs.Empty);
        }

        public event System.EventHandler OnCancelRequested;
        private void Cancel()
        {
            // Event trigger 
            OnCancelRequested?.Invoke(this, EventArgs.Empty);
            
        }
    }
}
