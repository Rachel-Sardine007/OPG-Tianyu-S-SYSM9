using OPG_Tianyu_Shi_SYSM9_CookMaster.Managers;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Models;
using OPG_Tianyu_Shi_SYSM9_CookMaster.ViewModels;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
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
    /// Interaction logic for RecipeDetailsWindow.xaml
    /// </summary>
    public partial class RecipeDetailsWindow : Window
    {
        public RecipeDetailsWindow(Recipe selectedRecipe)
        {
            InitializeComponent();
            var recipeManager = (RecipeManager)Application.Current.Resources["RecipeManager"];
            var vm = new RecipeDetailsViewModel(selectedRecipe, recipeManager);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Txb_Title.IsEnabled = true;
            Txb_Ing.IsEnabled = true;
            Txb_Ins.IsEnabled = true;
            Txb_Category.IsEnabled = true;
            Txb_Date.IsEnabled = true;
            Btn_Save.Visibility = Visibility.Visible;
            Btn_Edit.IsEnabled = false;
        }
    }
}
