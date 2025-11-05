using OPG_Tianyu_Shi_SYSM9_CookMaster.Managers;
using OPG_Tianyu_Shi_SYSM9_CookMaster.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.ViewModels
{
    public class ForgotPasswordViewModel: ViewModelBase
    {
        private readonly UserManager _userManager;
        private string _username;
        private string _newPassword;
        private string _securityQuestion;
        private string _securityAnswer;

        public string Username { 
            get =>  _username;
            set
            {
                _username = value;
                OnPropertyChanged();
                LoadSecurityQuestion();
            }
        }

        public string SecurityQuestion
        {
            get => _securityQuestion;
            set
            {
                _securityQuestion = value;
                OnPropertyChanged();
            }
        }

        public string SecurityAnswer
        {
            get => _securityAnswer;
            set
            {
                _securityAnswer = value;
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
            }
        }

        public ICommand ResetCommand { get; }

        public ForgotPasswordViewModel(UserManager userManager)
        {
            _userManager = userManager;
            ResetCommand = new RelayCommand(_=>ResetPassword());
        }

        public event System.EventHandler OnResetSuccess;
        private void ResetPassword()
        {
            _userManager.ResetPassword(Username, SecurityAnswer, NewPassword);
            OnResetSuccess?.Invoke(this, System.EventArgs.Empty);
        }

        private void LoadSecurityQuestion()
        {
            var user = _userManager.UserList.FirstOrDefault(u => u.Username == Username);
            SecurityQuestion = user?.SecurityQuestion ?? "No user found.";
        }
    }
}
