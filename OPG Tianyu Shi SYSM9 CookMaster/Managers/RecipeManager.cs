using OPG_Tianyu_Shi_SYSM9_CookMaster.Models;
using OPG_Tianyu_Shi_SYSM9_CookMaster.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.Managers
{
    public class RecipeManager: ViewModelBase
    {
        private readonly UserManager _userManager;
        private ObservableCollection<Recipe> _recipes;
        private Recipe _selectedRecipe;

        public ObservableCollection<Recipe> Recipes
        {
            get => _recipes;
            private set
            {
                _recipes = value;
                OnPropertyChanged();
            }
        }

        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            private set
            {
                _selectedRecipe = value;
                OnPropertyChanged();
            }
        }

        // Category list for filtering 
        public ObservableCollection<string> Categories
        {
            get
            {
                var uniqueCategories = _recipes
                    .Select(r => r.Category)
                    .Distinct()
                    .OrderBy(c => c)
                    .ToList();
                uniqueCategories.Insert(0, "All");
                return new ObservableCollection<string>(uniqueCategories);
            }
        }

        // Constructor
        public RecipeManager(UserManager userManager)
        {
            _userManager = userManager;
            var user = _userManager.CurrentUser;
            _recipes = new ObservableCollection<Recipe>();
            _recipes.Add(new Recipe
            {
                Title = "Banana Pancake",
                Ingredients = "Flour 100g, banana 2st, egg 1st, water 80g",
                Instructions = "Mix them all like a witch",
                Category = "Snack/Sweet",
                // Assigns year, month, day, hour, min, seconds, UTC timezone
                Date = new DateTime(2025, 10, 23, 10, 01, 29, DateTimeKind.Local),
                CreatedBy = "sardine"
            });
            _recipes.Add(new Recipe
            {
                Title = "Spaghetti Carbonara",
                Ingredients = "Spaghetti, egg, cheese, bacon, pepper",
                Instructions = "Cook pasta, mix ingredients.",
                Category = "Main Course",
                Date = DateTime.Now,
                CreatedBy = "admin"
            });

        }

        // Show recipe
        public ObservableCollection<Recipe> ShowRecipe()
        {
            string currentUsername = _userManager.CurrentUser.Username;

            if (currentUsername == "admin")
                return Recipes;
            

            // Filter by CreatedBy
            var userRecipes = _recipes
                .Where(r=> string.Equals(r.CreatedBy, currentUsername, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return new ObservableCollection<Recipe>(userRecipes);
        }
        
        public void AddRecipe(Recipe recipe)
        {
            _recipes.Add(recipe);
        }


        //public void UpdateRecipe(Recipe updatedRecipe)
        //{
        //    var existingRecipe = _recipes.FirstOrDefault(r => r.Title == updatedRecipe.Title);
        //    if (existingRecipe != null)
        //    {
        //        existingRecipe.Title = updatedRecipe.Title;
        //        existingRecipe.Ingredients = updatedRecipe.Ingredients;
        //        existingRecipe.Instructions = updatedRecipe.Instructions;
        //        existingRecipe.Category = updatedRecipe.Category;
        //        existingRecipe.Date = updatedRecipe.Date;
        //    }
        //}


    }
}
