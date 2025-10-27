using OPG_Tianyu_Shi_SYSM9_CookMaster.Service;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.Views.Pages
{
    /// <summary>
    /// Interaction logic for SplashPage.xaml
    /// </summary>
    public partial class SplashPage : Page
    {
        public SplashPage()
        {
            InitializeComponent();
            StartAnimation();
           
        }

        private void StartAnimation()
        {
            var fadeIn = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(1)));
            SplashImage.BeginAnimation(OpacityProperty, fadeIn);

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2.5) };
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                FadeOutAndNavigate();
            };
            timer.Start();
        }


        private void FadeOutAndNavigate()
        {
            var fadeOut = new DoubleAnimation(1,0, new Duration(TimeSpan.FromSeconds(1)));
            fadeOut.Completed += (s, e) => AppNavigator.Navigate(new LoginPage());
            this.BeginAnimation(OpacityProperty, fadeOut);
        }
    }
}
