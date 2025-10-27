using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.Service
{
    
    public static class AppNavigator
    {
        public static Frame MainFrame { get; set; }
        public static void Navigate(Page page)
        {
            MainFrame?.Navigate(page);
        }
    }

    
}
