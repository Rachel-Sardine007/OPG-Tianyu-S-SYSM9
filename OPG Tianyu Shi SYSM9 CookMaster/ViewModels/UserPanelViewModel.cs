using OPG_Tianyu_Shi_SYSM9_CookMaster.Managers;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Models;
using OPG_Tianyu_Shi_SYSM9_CookMaster.MVVM;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Service;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Views;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.ViewModels
{
    public class UserPanelViewModel: ViewModelBase
    {
        // Access to CurrentUser
        private readonly UserManager _userManager;
        public User CurrentUser => _userManager.CurrentUser;

        // ICommand Props
        public ICommand ViewRecipesCommand { get; }
        public ICommand AddRecipeCommand { get; }
        public ICommand ViewUserCommand { get; }
        public ICommand LogoutCommand { get; }

        
        // Constructor
        public UserPanelViewModel(UserManager userManager)
        {
            _userManager = userManager;

            ViewRecipesCommand = new RelayCommand(_ =>
            {
                var oldWindow = Application.Current.MainWindow;
                var recipeWindow = new RecipeListWindow();
                oldWindow.Close(); 
                recipeWindow.Show();
                
            });

            AddRecipeCommand = new RelayCommand(_ =>
            {
                var oldWindow = Application.Current.MainWindow;
                var addRecipeWindow = new AddRecipeWindow();
                oldWindow.Close();
                addRecipeWindow.Show();

            });

            ViewUserCommand = new RelayCommand(_ =>
            {
                var oldWindow = Application.Current.MainWindow;
                var userWindow = new UserDetailsWindow();
                oldWindow.Close();
                userWindow.Show();
            });

           LogoutCommand = new RelayCommand(_ =>
            {
                AppNavigator.Navigate(new LoginPage());
            });

        }

    }
}
