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
using System.Windows;
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

        public User CurrentUser
        {
            get => _currentUser;
            private set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        // Constructor
        public UserManager()
        {
            _users = new List<User>();
            // Call setDefaultUser() to add users
            SetDefaultUsers();

        }

        // ResetPassword
        public bool ResetPassword(string username, string answer, string newPassword)
        {
            var user = UserList.FirstOrDefault(u => u.Username == username);
            if (!FindUser(username))
            {
                MessageBox.Show("User does not exist!");
                return false;
            }

            if (!string.Equals(user.SecurityAnswer, answer, StringComparison.OrdinalIgnoreCase)){
                MessageBox.Show("Wrong answer!");
                return false;
            }

            user.Password = newPassword;
            MessageBox.Show("The password has been reset!");
            return true;
        }

        // DefaultUsers
        public void SetDefaultUsers()
        {
            // Load country list 
            var countries = CountryService.LoadCountryList();
            // Find country Items
            // var sweden = countries.FirstOrDefault(c=>c.Name == "Sweden");
            var china = countries.FirstOrDefault(c => c.Name == "China");

            _users.Add(new User
            {
                Username = "admin",
                Password = "password",
                Country = new CountryItem { Name="Sweden"},
                //Id = Guid.NewGuid()
            });

            var sardine = new User
            {
                Username = "sardine",
                Password = "sardine007",
                Country = china,
                //Id = Guid.NewGuid()
            };
            var user = new User
            {
                Username = "user",
                Password = "password",
                Country = new CountryItem { Name = "Sweden" },
                //Id = Guid.NewGuid()
                SecurityQuestion = "What's your nationality?",
                SecurityAnswer = "Sweden"
            };

            _users.Add(sardine);
            _users.Add(user);

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
        public void Logout() => CurrentUser = null;

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
        // RegisterViewModel
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
        // for persistence only 
        public void UpdateUser(string newUsername)
        {
            if (CurrentUser != null)
            {
                CurrentUser.Username = newUsername;
            }
            
        }

        // Method to control password
        // need to be updated: double check password --> property set
        public bool ValidatePassword(string password)
        {
            if (password.Length >= 8 && password.Any(ch => char.IsUpper(ch)) && password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                return true;
            }
            return false;
        }

        // for persistence only 
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
