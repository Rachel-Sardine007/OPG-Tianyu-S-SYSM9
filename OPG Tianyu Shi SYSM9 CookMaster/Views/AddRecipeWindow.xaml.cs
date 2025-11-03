using OPG_Tianyu_Shi_SYSM9_CookMaster.Managers;
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
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {
        public AddRecipeWindow()
        {
            InitializeComponent();
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            var recipeManager = new RecipeManager(userManager);
            var vm = new AddRecipeViewModel(recipeManager, userManager);
            DataContext = vm;
            vm.OnCancelRequested += Vm_OnCancelRequested;

        }

        private void Vm_OnCancelRequested(object? sender, EventArgs e)
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
    }
}
