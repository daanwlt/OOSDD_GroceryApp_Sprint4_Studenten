using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Application.Interfaces.Services;
using Grocery.Domain.Entities;
using Grocery.App.Services;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    /// <summary>
    /// ViewModel responsible for managing grocery list display and selection.
    /// Follows Single Responsibility Principle by having only one responsibility:
    /// managing grocery list UI state and user interactions.
    /// </summary>
    public partial class GroceryListViewModel : BaseViewModel
    {
        public ObservableCollection<GroceryList> GroceryLists { get; set; }
        public Client Client => _globalViewModel.Client;
        
        private readonly IGroceryListService _groceryListService;
        private readonly GlobalViewModel _globalViewModel;
        private readonly NavigationService _navigationService;
        private readonly RoleValidationService _roleValidationService;

        public GroceryListViewModel(
            IGroceryListService groceryListService, 
            GlobalViewModel globalViewModel,
            NavigationService navigationService,
            RoleValidationService roleValidationService) 
        {
            Title = "Boodschappenlijst";
            _groceryListService = groceryListService;
            _globalViewModel = globalViewModel;
            _navigationService = navigationService;
            _roleValidationService = roleValidationService;
            GroceryLists = new(_groceryListService.GetAll());
        }

        /// <summary>
        /// Handles grocery list selection by delegating to navigation service.
        /// This method has a single responsibility: handling user selection.
        /// </summary>
        /// <param name="groceryList">The selected grocery list</param>
        [RelayCommand]
        public async Task SelectGroceryList(GroceryList groceryList)
        {
            await _navigationService.NavigateToGroceryListItems(groceryList);
        }

        /// <summary>
        /// Refreshes grocery lists when the view appears.
        /// This method has a single responsibility: data refresh.
        /// </summary>
        public override void OnAppearing()
        {
            base.OnAppearing();
            RefreshGroceryLists();
        }

        /// <summary>
        /// Clears grocery lists when the view disappears.
        /// This method has a single responsibility: cleanup.
        /// </summary>
        public override void OnDisappearing()
        {
            base.OnDisappearing();
            GroceryLists.Clear();
        }

        /// <summary>
        /// Shows bought products if user has admin role.
        /// This method has a single responsibility: conditional navigation.
        /// </summary>
        [RelayCommand]
        public async Task ShowBoughtProducts()
        {
            if (_roleValidationService.CanAccessAdminFeatures(_globalViewModel.Client))
            {
                await _navigationService.NavigateToBoughtProducts();
            }
        }

        /// <summary>
        /// Refreshes the grocery lists collection.
        /// This method has a single responsibility: data refresh.
        /// </summary>
        private void RefreshGroceryLists()
        {
            GroceryLists = new(_groceryListService.GetAll());
        }
    }
}
