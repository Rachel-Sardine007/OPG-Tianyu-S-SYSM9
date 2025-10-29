using OPG_Tianyu_Shi_SYSM9_CookMaster.Models;
using OPG_Tianyu_Shi_SYSM9_CookMaster.MVVM;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Service;
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
        public List<User> UserList { get { return _users; } }

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
            private set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        // DefaultUsers
        public void SetDefaultUsers()
        {
            // Load country list 
            var countries = CountryService.LoadCountryList();
            // Find country Items
            var sweden = countries.FirstOrDefault(c=>c.Name == "Sweden");
            var china = countries.FirstOrDefault(c => c.Name == "China");

            _users.Add(new User
            {
                Username = "admin",
                Password = "admin123",
                Country = sweden
            });
            _users.Add(new User
            {
                Username = "sardine",
                Password = "sardine007",
                Country = china
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
        public void Register(string username, string password, CountryItem country)
        {
            _users.Add(new User
            {
                Username = username,
                Password = password,
                Country = country
            });
        }

        // Method to check if username already exists and return true if user doesnt exist
        // Register
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

        
        // UserDetails update current user info
        public void UpdateUser(string newUsername, CountryItem newCountry)
        {
            var existingUser = _users.FirstOrDefault(u => u.Username == CurrentUser.Username);
            if (existingUser != null)
            {
                existingUser.Username = newUsername;
                existingUser.Country = newCountry;
                CurrentUser = existingUser; // refresh binding 
            }
        }

        public void ChangePassword(string username, string password)
        {
            var existingUser = _users.FirstOrDefault(u => u.Username == username);
            if (existingUser != null)
            {
                existingUser.Password = password;
            }
        }
    }
}
