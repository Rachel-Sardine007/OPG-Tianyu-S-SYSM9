using OPG_Tianyu_Shi_SYSM9_CookMaster.Models;
using OPG_Tianyu_Shi_SYSM9_CookMaster.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.Managers
{
    public class RecipeManager: ViewModelBase
    {
        // New ObservableCollection
        public ObservableCollection<Recipe> _recipes { get; set; }

        // 
        public RecipeManager()
        {
            _recipes = new ObservableCollection<Recipe>();
            _recipes.Add(new Recipe{
                Title = "Banana Pancake",
                Ingredients = "Flour 100g, banana 2st, egg 1st, water 80g",
                Instructions = "Mix them all like a witch",
                Category = "Snack/Sweet",
                Date = DateTime.Now, // change later
                //CreatedBy = UserManager.UserList[1].Username // accessbility error
            });
        }
    }
}
