using OPG_Tianyu_Shi_SYSM9_CookMaster.Managers;
using System.Configuration;
using System.Data;
using System.Windows;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var userManager = new UserManager();
            var recipeManager = new RecipeManager(userManager);

            Application.Current.Resources["UserManager"] = userManager;
            Application.Current.Resources["RecipeManager"] = recipeManager;


            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }

}
