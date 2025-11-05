using OPG_Tianyu_Shi_SYSM9_CookMaster.Managers;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Service;
using OPG_Tianyu_Shi_SYSM9_CookMaster.ViewModels;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.Views
{
    /// <summary>
    /// Interaction logic for ForgotPasswordWindow.xaml
    /// </summary>
    public partial class ForgotPasswordWindow : Window
    {
        public ForgotPasswordWindow(UserManager userManager)
        {
            InitializeComponent();
            var viewModel = new ForgotPasswordViewModel(userManager);
            DataContext = viewModel;

            viewModel.OnResetSuccess += ViewModel_OnResetSuccess;
        }

        private void ViewModel_OnResetSuccess(object? sender, EventArgs e)
        {
            var newMain = Application.Current.Windows
                .OfType<MainWindow>()
                .FirstOrDefault();

            if (newMain == null)
            {
                newMain = new MainWindow();
                Application.Current.MainWindow = newMain;
                newMain.Show(); // make sure mainfram is visible
            }
            else
            {
                newMain.Show();
                newMain.Activate();
            }

            AppNavigator.Navigate(new LoginPage()); // navigate to login page
            this.Close();
        }

        private void Pwd_NewPwd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ForgotPasswordViewModel viewModel) viewModel.NewPassword = Pwd_NewPwd.Password;
        }
    }
}
