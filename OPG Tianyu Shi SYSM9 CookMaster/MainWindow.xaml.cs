using OPG_Tianyu_Shi_SYSM9_CookMaster.Managers;
using OPG_Tianyu_Shi_SYSM9_CookMaster.ViewModels;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Views;
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
            DataContext = new MainViewModel(userManager);
        }

        // PasswordBox 
        private void Pwd_PasswordChanged(object s, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel viewModel) viewModel.Password = Pwd.Password;
        }

        private void Btn_Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow= new RegisterWindow();
            var oldWindow = Application.Current.MainWindow;
            oldWindow.Close();
            registerWindow.Show();

        }

        private void Btn_Login_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}