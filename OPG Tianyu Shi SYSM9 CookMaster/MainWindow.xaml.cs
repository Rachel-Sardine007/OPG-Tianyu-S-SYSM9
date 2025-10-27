using OPG_Tianyu_Shi_SYSM9_CookMaster.Managers;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Service;
using OPG_Tianyu_Shi_SYSM9_CookMaster.ViewModels;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Views;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Views.Pages;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            var viewModel = new MainViewModel(userManager);
            DataContext = viewModel;


            // Make the Frame globally available
            AppNavigator.MainFrame = MainFrame;
            // Load Splashpage as the first page
            MainFrame.Navigate(new SplashPage());

        }


    

    }
}