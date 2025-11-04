using OPG_Tianyu_Shi_SYSM9_CookMaster.Managers;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Models;
using OPG_Tianyu_Shi_SYSM9_CookMaster.MVVM;
using OPG_Tianyu_Shi_SYSM9_CookMaster.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.ViewModels
{
    public class AddRecipeViewModel: ViewModelBase
    {
        private readonly RecipeManager _recipeManager;
        private string _title;
        private string _ingredients;
        private string _instructions;
        private string _category;
        private DateTime _selectedDate = DateTime.Now; 
        
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }
        public string Ingredients
        {
            get => _ingredients;
            set
            {
                _ingredients = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }
        public string Instructions 
        {
            get => _instructions; 
            set
            {
                _instructions = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }
        public string Category 
        { 
            get => _category; 
            set 
            { _category = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            } 
        }
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged();
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }

        // Constructor
        public AddRecipeViewModel(RecipeManager recipeManager)
        {
            _recipeManager = recipeManager;
            AddCommand = new RelayCommand(_ => AddRecipe(), 
                canExecute => CanAdd());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        public event System.EventHandler OnCancelRequested;
        private void Cancel()
        {
            OnCancelRequested?.Invoke(this, EventArgs.Empty);
        }

        private bool CanAdd()
        {
            if (!string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Ingredients)
                && !string.IsNullOrWhiteSpace(Instructions))
            {
                return true;
            }
                return false;
        }

        public event System.EventHandler OnAddRecipeSuccess;
        private void AddRecipe()
        {
            _recipeManager.AddRecipe(Title,Ingredients,Instructions,Category,SelectedDate);
            MessageBox.Show("New recipe added!");
            OnAddRecipeSuccess?.Invoke(this, System.EventArgs.Empty);
        }
    }
}
