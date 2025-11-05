using OPG_Tianyu_Shi_SYSM9_CookMaster.Managers;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Service;
using OPG_Tianyu_Shi_SYSM9_CookMaster.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.Views.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            var viewModel = new MainViewModel(userManager);
            viewModel.OnLoginSuccess += ViewModel_OnLoginSuccess;
            viewModel.OpenForgotPwdWindowRequest += ViewModel_OpenForgotPwdWindowRequest;
            DataContext = viewModel;
        }

        private void ViewModel_OpenForgotPwdWindowRequest(object? sender, EventArgs e)
        {
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            var forgotPasswordWindow = new ForgotPasswordWindow(userManager);
            var oldWindow = Application.Current.MainWindow;
            oldWindow.Close();
            forgotPasswordWindow.ShowDialog();
        }

        private void ViewModel_OnLoginSuccess(object? sender, EventArgs e)
        {
            //// Load UserPanelPage
            //AppNavigator.Navigate(new UserPanelPage());

            // Direct to RecipeListWindow 
            RecipeListWindow recipeListWindow = new RecipeListWindow();
            var oldWindow = Application.Current.MainWindow;
            oldWindow.Close();
            recipeListWindow.Show();
        }

        // PasswordBox 
        private void Pwd_PasswordChanged(object s, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel viewModel) viewModel.Password = Pwd.Password;
        }

        private void Btn_Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            var oldWindow = Application.Current.MainWindow;
            oldWindow.Close();
            registerWindow.Show();
        }

    }
}
