using OPG_Tianyu_Shi_SYSM9_CookMaster.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.Managers
{
    public class UserManager
    {
        // Property
        private List<User> _users;
        private User _currentUser;

        // Constructor
        public UserManager()
        {
            _users = new List<User>();

        }

        // Value setting to CurrentUser
        public User CurrentUser
        {
            get => _currentUser;
            private set { _currentUser = value; }
        }

        // DefaultUsers
        private void SetDefaultUsers()
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

        // Method to check if username already exists
        public bool FindUser(string username)
        {
            foreach (var user in _users)
            {
                if (!string.Equals(user.Username, username, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        // Method to control password
        public bool ValidLength(string password)
        {
            return password.Length >= 8;
        }
        public bool CheckVersal(string password)
        {
            return password.Any(ch => char.IsUpper(ch));
        }
        public bool CheckChar(string password)
        {
            // using linq and Any method
            return password.Any(ch => !char.IsLetterOrDigit(ch));
        }

        // Method to change password for CurrentUser
        public void ChangePassword(string usesrname, string password)
        {
            if (ValidLength(password) && CheckVersal(password) && CheckChar(usesrname))
            {
                CurrentUser.Password = password;
            }  
        }

        // Method for getting logged-in user info
        public void GetLoggedIn()
        {
            
        }
    }
}
