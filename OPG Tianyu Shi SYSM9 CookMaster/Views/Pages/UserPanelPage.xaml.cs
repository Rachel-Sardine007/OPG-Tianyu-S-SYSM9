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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.Views.Pages
{
    /// <summary>
    /// Interaction logic for UserPanelPage.xaml
    /// </summary>
    public partial class UserPanelPage : Page
    {
        public UserPanelPage()
        {
            InitializeComponent();
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            DataContext = new MainViewModel(userManager);
 
        }
    }
}
