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
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {

        public UserDetailsWindow()
        {
            InitializeComponent();
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            var vm = new UserDetailsViewModel(userManager);
            vm.OnCancelRequested += Vm_OnCancelRequested;
            vm.OnSaveSuccess += Vm_OnSaveSuccess;
            vm.OnPasswordChanged += Vm_OnPasswordChanged;
            DataContext = vm;
        }

        private void Vm_OnCancelRequested(object? sender, EventArgs e)
        {
            ReturnToRecipeList();
        }

        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            TxB_Username.IsEnabled = true;
            CmB_Country.IsEnabled = true;
            Btn_Save.Visibility = Visibility.Visible;
            Btn_Edit.IsEnabled = false;
        }

        private void Vm_OnPasswordChanged(object? sender, EventArgs e)
        {
            // MessageBox
            var result = MessageBox.Show(
                "Your Password has been changed!",
                "Updated successful",
                MessageBoxButton.OK);

            if (result == MessageBoxResult.OK)
            {
                ReturnToRecipeList();
            }
        }

        private void Vm_OnSaveSuccess(object? sender, EventArgs e)
        {
            // MessageBox
            var result = MessageBox.Show(
                "Your information has been updated!",
                "Updated successful!",
                MessageBoxButton.OK);

            if (result == MessageBoxResult.OK)
            {
                ReturnToRecipeList();
            }
        }

        private void ReturnToRecipeList()
        {
            var currentWindow = Application.Current.Windows
                .OfType<Window>()
                .SingleOrDefault(x => x.IsActive);
            if (currentWindow != null)
            {
                var newWindow = new RecipeListWindow();
                Application.Current.MainWindow = newWindow;
                newWindow.Show();
                currentWindow.Close();
            }
            this.Close();
        }

        private void Pwb_Current_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is UserDetailsViewModel vm) vm.CurrentPassword = Pwb_Current.Password;
        }

        private void Pwb_New_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is UserDetailsViewModel vm) vm.NewPassword = Pwb_New.Password;
        }

        private void Pwb_Confirm_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is UserDetailsViewModel vm) vm.ConfirmPassword = Pwb_Confirm.Password;
        }
    }
}
