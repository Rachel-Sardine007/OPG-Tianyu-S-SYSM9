using OPG_Tianyu_Shi_SYSM9_CookMaster.Models;
using OPG_Tianyu_Shi_SYSM9_CookMaster.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.Managers
{
    // Implement interface
    public class UserManager : ViewModelBase
    {
        // Property
        private List<User> _users;
        private User _currentUser;

        // public list for reference use later
        public List<User> UserList {  get { return _users; } }
        public User Current { get { return _currentUser; } }

        // Constructor
        public UserManager()
        {
            _users = new List<User>();

            // Call setDefaultUser()
            SetDefaultUsers();

        }

        // Value setting to CurrentUser
        public User CurrentUser
        {
            get => _currentUser;
            private set {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        // DefaultUsers
        public void SetDefaultUsers()
        {
            _users.Add(new User
            {
                Username = "admin",
                Password = "admin123",
                Country = "Sweden"
            });
            _users.Add(new User
            {
                Username = "sardine",
                Password = "sardine007",
                Country = "China"
            });
        }

        // Method to check login status and return true of false
        public bool Login(string username, string password)
        {
            foreach (var user in _users)
            {
                if (string.Equals(user.Username, username, StringComparison.OrdinalIgnoreCase)
                    && user.Password == password)
                {
                    CurrentUser = user;
                    return true;
                }
            }
            return false;
        }

        // Logout method
        public void Logout()
        {
            CurrentUser = null;
        }

        // Methods for registering new users 
        // Method to add new user information
        public void Register(string username, string password, string country)
        {
            _users.Add(new User
            {
                Username = username,
                Password = password,
                Country = country
            });
        }

        // Method to check if username already exists and return true if user doesnt exist
        public bool FindUser(string username)
        {
            foreach (var user in _users)
            {
                if (string.Equals(user.Username, username, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        // Method to control password
        // !! need to be updated: double check password
        public bool ValidatePassword(string password)
        {
            if (password.Length >= 8 && password.Any(ch => char.IsUpper(ch)) && password.Any(ch => !char.IsLetterOrDigit(ch))){
                return true;
            }
            return false;
        }
    }
}
