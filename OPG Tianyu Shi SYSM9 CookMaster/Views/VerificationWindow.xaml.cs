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
using System.Windows.Shapes;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.Views
{
    /// <summary>
    /// Interaction logic for VerificationWindow.xaml
    /// </summary>
    public partial class VerificationWindow : Window
    {
        public VerificationWindow()
        {
            InitializeComponent();
            var vm = new VerificationViewModel();
            DataContext = vm;
            vm.OnLoginSuccess += Vm_OnLoginSuccess;
        }

        private void Vm_OnLoginSuccess(object? sender, EventArgs e)
        {
            var currentWindow = Application.Current.Windows
                   .OfType<Window>()
                   .SingleOrDefault(x => x.IsActive);
            if (currentWindow != null)
            {
                var recipeListWindow = new RecipeListWindow();
                Application.Current.MainWindow = recipeListWindow;
                recipeListWindow.Show();
                currentWindow.Close();
            }
        }
    }
}
