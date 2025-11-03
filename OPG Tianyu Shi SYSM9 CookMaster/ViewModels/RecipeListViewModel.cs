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
        public User CurrentUser => _userManager.CurrentUser;

        public ObservableCollection<Recipe> Recipes { get; set; }

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

        public ObservableCollection<string> Categories => _recipeManager.Categories;

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

        // Filter method
        private void ApplyFilter()
        {
            if (string.IsNullOrEmpty(_selectedCategory) || SelectedCategory =="All")
            {
                RecipesView.Filter = null;
            }
            else
            {
                RecipesView.Filter = item =>
                {
                    var recipe = item as Recipe;
                    return recipe != null && recipe.Category == _selectedCategory;
                };
            }
            RecipesView.Refresh();
        }

        public ICommand UserCommand { get; }
        public ICommand AddRecipeCommand {  get; }
        public ICommand RecipeDetailsCommand { get; }
        public ICommand RemoveRecipeCommand {  get; }
        public ICommand SignOutCommand { get; }
        public ICommand ReturnCommand {  get; }

        // CollectionView for filtering
        public ICollectionView RecipesView {  get; }

        public RecipeListViewModel(RecipeManager recipeManager, UserManager userManager)
        {
            _recipeManager = recipeManager;
            _userManager = userManager;
            _recipeManager.LoadDefaultRecipes();
            LoadRecipes();
           
            RecipesView= CollectionViewSource.GetDefaultView(Recipes);

            UserCommand = new RelayCommand(execute =>
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
            });

            AddRecipeCommand = new RelayCommand(_ =>
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
            });

            RecipeDetailsCommand = new RelayCommand(excute => OpenDetails(),
                canExcute => SelectedRecipe != null);

            RemoveRecipeCommand = new RelayCommand(excute => RemoveRecipe(), canExecute => SelectedRecipe != null);

            SignOutCommand = new RelayCommand(_ =>
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
            });
        }

        private void LoadRecipes()
        {
            var userRecipes = _recipeManager.ShowRecipe();
            Recipes = new ObservableCollection<Recipe>(userRecipes);
        }

        private void OpenDetails()
        {
            var recipe = SelectedRecipe;
            if (recipe == null) return;

            var detailsVm = new RecipeDetailsViewModel(recipe, _recipeManager);

            var newWindow = new RecipeDetailsWindow
            {
                DataContext = detailsVm
            };
            newWindow.ShowDialog();
        }

        private void RemoveRecipe()
        {
            Recipes.Remove(SelectedRecipe);
        }

    }
}
