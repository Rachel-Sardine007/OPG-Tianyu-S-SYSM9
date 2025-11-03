using OPG_Tianyu_Shi_SYSM9_CookMaster.Managers;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Models;
using OPG_Tianyu_Shi_SYSM9_CookMaster.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Input;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.ViewModels
{
    public class RecipeDetailsViewModel : ViewModelBase
    {
        private readonly RecipeManager _recipeManager;
        private Recipe _selectedRecipe;
        private string _error;

        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set
            {
                _selectedRecipe = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _selectedRecipe.Title;
            set
            {
                if (_selectedRecipe.Title != value)
                {
                    _selectedRecipe.Title = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Ingredients
        {
            get => _selectedRecipe.Ingredients;
            set
            {
                if (_selectedRecipe.Ingredients != value)
                {
                    _selectedRecipe.Ingredients = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Instructions
        {
            get => _selectedRecipe.Instructions;
            set
            {
                if (_selectedRecipe.Instructions != value)
                {
                    _selectedRecipe.Instructions = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Category
        {
            get => _selectedRecipe.Category;
            set
            {
                if (_selectedRecipe.Category != value)
                {
                    _selectedRecipe.Category = value;
                    OnPropertyChanged();
                }

            }
        }

        public DateTime SelectedDate
        {
            get => _selectedRecipe.Date;
            set
            {
                if (_selectedRecipe.Date != value)
                {
                    _selectedRecipe.Date = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Error
        {
            get => _error;
            set
            {
                _error = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }


        public RecipeDetailsViewModel(Recipe selectedRecipe, RecipeManager recipeManager)
        {
            _recipeManager = recipeManager;
            _selectedRecipe = selectedRecipe;
            SaveCommand = new RelayCommand(_ => Save());
            CancelCommand = new RelayCommand(_ => Cancel());

        }

        private void Save()
        {
            MessageBox.Show("Recipe saved successfully!", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public event System.EventHandler OnCancelRequested;
        private void Cancel()
        {
            OnCancelRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
