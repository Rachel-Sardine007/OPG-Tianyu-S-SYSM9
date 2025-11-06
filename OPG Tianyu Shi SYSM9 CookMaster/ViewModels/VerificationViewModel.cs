using OPG_Tianyu_Shi_SYSM9_CookMaster.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.ViewModels
{
    public class VerificationViewModel: ViewModelBase
    {
        private string _code;
        private string _verificationCode;
        private string _error;
        
        public string Code
        {
            get => _code;
            set { 
                _code = value; 
                OnPropertyChanged(); 
            }
        }
        public string VerificationCode
        {
            get => _verificationCode;
            private set
            {
                _verificationCode = value;
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

        public ICommand ConfirmCommand { get; set; }

        public VerificationViewModel()
        {
            ShowCode();
            ConfirmCommand = new RelayCommand(_ => ConfirmCode());
        }

        public void ShowCode()
        {
            Random random = new Random();
            _verificationCode = random.Next(1000000).ToString("D6");
            MessageBox.Show(string.Format("Verification code: {0}", VerificationCode),
                "Verification",
                MessageBoxButton.OK);
        }

        public event System.EventHandler OnLoginSuccess;
        private void ConfirmCode()
        {
            if (Code == null)
            {
                Error = "Please enter verification code!";
                return;
            }
            
            if(Code != VerificationCode)
            {
                Error = "Incorrect verification code!";
                return;
            }

            Error = string.Empty;
            MessageBox.Show("Verify successfully!");
            OnLoginSuccess?.Invoke(this, System.EventArgs.Empty);
        }
    }
}
