# 🍳 CookMaster

CookMaster is a WPF desktop application built for managing and organizing personal recipes using the MVVM pattern.  
It allows multiple users to register, log in, and create, edit, or view their own recipes within an intuitive interface.
This project demonstrates data binding, command handling, and user-friendly UI design in C#.

---

## 📸 Features

- 👤 **User Management**
  - Register and log in users with 2FA.
  - Save and load user data.
  - Admin users can view all recipes.

- 🍲 **Recipe Management**
  - Add, edit, and delete recipes.
  - Filter by category or date.
  - Copy existing recipes for quick duplication.

- 🪄 **MVVM Architecture**
  - Clean separation between UI and logic.
  - Uses ViewModels with `RelayCommand` for button and menu actions.

---

## 🏗️ Architecture Overview
CookMaster/
│
├── Models/
│   ├── User.cs
│   └── Recipe.cs
│   
├── Managers/
│   ├── UserManager.cs
│   └── RecipeManager.cs
│
├── ViewModels/
│   ├── MainViewModel.cs
│   ├── RegisterViewModel.cs
│   ├── // UserPanelViewModel.cs
│   ├── RecipeListViewModel.cs
│   ├── RecipeDetailsViewModel.cs
│   ├── AddRecipeViewModel.cs
│   ├── ForgotPasswordViewModel.cs
│   ├── VerificationViewModel.cs
│   └── UserDetailsViewModel.cs
│
├── Views/
│   ├── MainWindow.xaml
│   ├── SplashPage.xaml
│   ├── LoginPage.xaml
│   ├── RegisterWindow.xaml
│   ├── // UserPanelPage.xaml
│   ├── RecipeListWindow.xaml
│   ├── RecipeDetailsWindow.xaml
│   ├── AddRecipeWindow.xaml
│   ├── ForgotPasswordWindow.xaml
│   ├── VerificationWindow.xaml
│   └── UserDetailsWindow.xaml
│
├── Services/
│   ├── AppNavigatior.cs
│   ├── TextBoxHelper.cs
│   └── CountryList.cs
│
├── Assets/
│   └── CountryList.xml
│
└── App.xaml / App.xaml.cs

- 📈 **Future Improvements**
  - Improve code efficiency and refactor repetitive logic.
  - Enhance the UI/UX with modern styling and better layout consistency.
  - Implement data persistence (e.g., save recipes to JSON or database).
  - Add image support or ingredient import features.

