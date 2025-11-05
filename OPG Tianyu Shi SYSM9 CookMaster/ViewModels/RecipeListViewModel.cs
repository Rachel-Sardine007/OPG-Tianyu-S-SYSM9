using OPG_Tianyu_Shi_SYSM9_CookMaster.Managers;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Models;
using OPG_Tianyu_Shi_SYSM9_CookMaster.MVVM;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Service;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Views;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.ViewModels
{
    public class RecipeListViewModel:ViewModelBase
    {
        private readonly UserManager _userManager;
        private readonly RecipeManager _recipeManager;
        private ObservableCollection<Recipe> _recipes;
        public ObservableCollection<Recipe> Recipes => _recipeManager.UserRecipes;

        public ObservableCollection<string> CategoryList => _recipeManager.Categories; // UI binding

        private Recipe _selectedRecipe;
        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set
            {
                _selectedRecipe = value;
                OnPropertyChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        private string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        public ICommand UserCommand { get; }
        public ICommand AddRecipeCommand {  get; }
        public ICommand RecipeDetailsCommand { get; }
        public ICommand RemoveRecipeCommand {  get; }
        public ICommand SignOutCommand { get; }
        public ICommand InfoCommand {  get; }

        // CollectionView for filtering
        public ICollectionView RecipesView {  get; }

        public User CurrentUser => _userManager.CurrentUser; // UI binding

        public RecipeListViewModel(RecipeManager recipeManager, UserManager userManager)
        {
            _recipeManager = recipeManager;
            _userManager = userManager;

            LoadRecipes();
           
            RecipesView= CollectionViewSource.GetDefaultView(Recipes);

            InfoCommand = new RelayCommand(_ => OpenInfo());
            UserCommand = new RelayCommand(execute => OpenUserDetails());
            AddRecipeCommand = new RelayCommand(_ => OpenAddRecipe());
            RecipeDetailsCommand = new RelayCommand(excute => OpenRecipeDetails(),
                canExcute => SelectedRecipe != null);
            RemoveRecipeCommand = new RelayCommand(excute => RemoveRecipe(), 
                canExecute => SelectedRecipe != null);
            SignOutCommand = new RelayCommand(_ => SignOut());
        }

        // Filter method
        private void ApplyFilter()
        {
            RecipesView.Filter = item =>
            {
                if (item is not Recipe recipe)
                    return false;

                bool categoryMatch = _selectedCategory == "All" || recipe.Category == _selectedCategory;

                bool searchMatch = string.IsNullOrWhiteSpace(_searchText)
                    || recipe.Title.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                    || recipe.Ingredients.Contains(_searchText, StringComparison.OrdinalIgnoreCase);

                bool dateMatch = !_selectedDate.HasValue || recipe.Date.Date == _selectedDate.Value.Date;

                return categoryMatch && searchMatch && dateMatch;
            };
            RecipesView.Refresh();
        }

        private void OpenInfo()
        {
            MessageBox.Show(
                "This application allows you to manage and explore your personal recipe collection. " +
                "Filter recipes by category, and sort them by date. " +
                "Click on a recipe to view details or edit it. Use the toolbar buttons by right side to manage your account.",
                "About this app",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void SignOut()
        {
            var currentWindow = Application.Current.Windows
                .OfType<Window>()
                .SingleOrDefault(x => x.IsActive);
            if (currentWindow != null)
            {
                var mainWindow = new MainWindow();
                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();
                currentWindow.Close();
            }
            _userManager.Logout();
        }

        private void OpenAddRecipe()
        {
            var currentWindow = Application.Current.Windows
                   .OfType<Window>()
                   .SingleOrDefault(x => x.IsActive);
            if (currentWindow != null)
            {
                var addRecipeWindow = new AddRecipeWindow();
                Application.Current.MainWindow = addRecipeWindow;
                addRecipeWindow.Show();
                currentWindow.Close();
            }
        }

        private void OpenUserDetails()
        {
            // Close current window
            var currentWindow = Application.Current.Windows
                .OfType<Window>()
                .SingleOrDefault(x => x.IsActive);
            if (currentWindow != null)
            {
                var UserDetailsWindow = new UserDetailsWindow();
                Application.Current.MainWindow = UserDetailsWindow;
                UserDetailsWindow.Show();
                currentWindow.Close();
            }
        }

        private void LoadRecipes()
        {
           _recipeManager.ShowRecipe();
        }

        private void OpenRecipeDetails()
        {
            var recipe = SelectedRecipe;
            if (recipe == null) return;

            var currentWindow = Application.Current.Windows
                .OfType<Window>()
                .SingleOrDefault(x => x.IsActive);
            if (currentWindow != null)
            {
                var recipeDetailsWindow = new RecipeDetailsWindow(_selectedRecipe);
                recipeDetailsWindow.Show();
                currentWindow.Close();
            }
        }

        private void RemoveRecipe()
        {
            _recipes.Remove(SelectedRecipe);
        }

    }
}
