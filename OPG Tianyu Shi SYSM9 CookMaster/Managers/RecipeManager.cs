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
        private ObservableCollection<Recipe> _userRecipes;
        public ObservableCollection<Recipe> Recipes => _recipes;
        public ObservableCollection<Recipe> UserRecipes
        {
            get => _userRecipes;
            set
            {
                _userRecipes = value;
                OnPropertyChanged();
            }
        }

        // Category list for filtering 
        public ObservableCollection<string> Categories
        {
            get
            {
                var uniqueCategories = UserRecipes
                    .Select(r => r.Category)
                    .Distinct()
                    .OrderBy(c => c)
                    .ToList();
                uniqueCategories.Insert(0, "All");
                return new ObservableCollection<string>(uniqueCategories);
            }
            set
            {
                OnPropertyChanged();
            }
        }

        // Constructor
        public RecipeManager(UserManager userManager)
        {
            _userManager = userManager;
            _recipes = new ObservableCollection<Recipe>();
            
            LoadDefaultRecipes();
        }

        public void LoadDefaultRecipes()
        {
            _recipes.Add(new Recipe
                {
                    Title = "Banana Pancake",
                    Ingredients = "Flour 100g, banana 2st, egg 1st, water 80g",
                    Instructions = "Mix them all like a witch",
                    Category = "Snack/Sweet",
                    // Assigns year, month, day, hour, min, seconds, UTC timezone
                    Date = new DateTime(2025, 10, 23, 10, 01, 29, DateTimeKind.Local),
                    CreatedBy = _userManager.UserList[1].Username,
            });
            
            _recipes.Add(new Recipe
            {
                Title = "Spaghetti Carbonara",
                Ingredients = "Spaghetti, egg, cheese, bacon, pepper",
                Instructions = "Cook pasta, mix ingredients.",
                Category = "Main Course",
                Date = DateTime.Now,
                CreatedBy = _userManager.UserList[2].Username, // can be more flexible if based on unique userID
            });

            _recipes.Add(new Recipe
            {
                Title = "Pan fried beef steak",
                Ingredients = "Beef, butter, garlic",
                Instructions = "Pan fry both side of the steak for 1 min with butter and garlic. Don't let Chef Gordon down",
                Category = "Main Course",
                Date = DateTime.Now,
                CreatedBy = _userManager.UserList[2].Username,
            });
        }

        // Show recipe
        public void ShowRecipe()
        {
            var user = _userManager.CurrentUser;
            if (user == null) return;
            if (user.Username == "admin")
            {
                _userRecipes = new ObservableCollection<Recipe>(_recipes);
            }
            else
            {
                // Filter by username
                _userRecipes = new ObservableCollection<Recipe>(_recipes
                    .Where(r => r.CreatedBy == user.Username));
            }
        }

        public void AddRecipe(string title,string ingredients, string instructions, string category, DateTime date)
        {
            var user = _userManager.CurrentUser;
            _recipes.Add(new Recipe
            {
                Title = title,
                Ingredients = ingredients,
                Instructions = instructions,
                Category = category,
                Date = date,
                CreatedBy = user.Username
            });

            ShowRecipe(); // refresh user-specific recipe list
        }

        // Update CreatedBy if username changes
        public void UpdateCreatedBy(string oldUsername, string newUsername)
        {
            foreach (var r in Recipes
                .Where(r => r.CreatedBy == oldUsername))
            {
                r.CreatedBy = newUsername;   
            }
        }
    }
}
