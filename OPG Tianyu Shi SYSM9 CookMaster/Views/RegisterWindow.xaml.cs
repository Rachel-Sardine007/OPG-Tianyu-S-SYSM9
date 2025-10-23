using OPG_Tianyu_Shi_SYSM9_CookMaster.Managers;
using OPG_Tianyu_Shi_SYSM9_CookMaster.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
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
using System.Xml.Linq;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.Views
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            var viewModel = new RegisterViewModel(userManager);
            viewModel.OnRegisterSuccess += RegisterViewModel_OnRegisterSuccess;
            
            DataContext = viewModel;
            LoadCountryList();
            
        }

        private void RegisterViewModel_OnRegisterSuccess(object? sender, EventArgs e)
        {
            var newMain = new MainWindow();
            newMain.Show();

            this.Close();
        }

        // Load CountryList
        private void LoadCountryList()
        {
            var doc = XDocument.Load("C:\\Mac\\Home\\Documents\\Csharp101\\Inluppg\\OPG Tianyu Shi SYSM9 CookMaster\\OPG Tianyu Shi SYSM9 CookMaster\\CountryList.xml");
            var countryList = from c in doc.Descendants("country")
                              select new
                              {
                                  Name = c.Element("short_desc")?.Value,
                                  Code = c.Element("country_code")?.Value

                              };
            Cmb_Country.ItemsSource = countryList.ToList();
            Cmb_Country.DisplayMemberPath = "Name";
            Cmb_Country.SelectedValuePath = "Code";
            // MessageBox to check how many countries are loaded successfully
            // MessageBox.Show("Loaded countries: " + countryList.Count());

        }

        private void Pwd1_PasswordChanged(object s, RoutedEventArgs e)
        {
            if (DataContext is RegisterViewModel viewModel) viewModel.Password1 = Pwd1.Password;
        }

        private void Pwd2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterViewModel viewModel) viewModel.Password2 = Pwd2.Password;
        }

        private void Btn_BackToMain_Click(object sender, RoutedEventArgs e)
        {
            var newMain = new MainWindow();
            newMain.Show();

            this.Close();
        }
    }
}
