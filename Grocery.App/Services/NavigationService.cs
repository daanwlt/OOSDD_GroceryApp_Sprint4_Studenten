using Grocery.Domain.Entities;

namespace Grocery.App.Services
{
    /// <summary>
    /// Service responsible for handling navigation operations.
    /// Follows Single Responsibility Principle by having only one responsibility:
    /// managing application navigation logic.
    /// </summary>
    public class NavigationService
    {
        /// <summary>
        /// Navigates to the grocery list items view for a specific grocery list.
        /// This method has a single responsibility: navigation to grocery list items.
        /// </summary>
        /// <param name="groceryList">The grocery list to navigate to</param>
        /// <returns>Task representing the navigation operation</returns>
        public async Task NavigateToGroceryListItems(GroceryList groceryList)
        {
            Dictionary<string, object> parameters = new() { { nameof(GroceryList), groceryList } };
            await Shell.Current.GoToAsync($"{nameof(Views.GroceryListItemsView)}?Titel={groceryList.Name}", true, parameters);
        }

        /// <summary>
        /// Navigates to the bought products view.
        /// This method has a single responsibility: navigation to bought products.
        /// </summary>
        /// <returns>Task representing the navigation operation</returns>
        public async Task NavigateToBoughtProducts()
        {
            await Shell.Current.GoToAsync(nameof(Views.BoughtProductsView));
        }

        /// <summary>
        /// Navigates to the main application shell after successful login.
        /// This method has a single responsibility: navigation after authentication.
        /// </summary>
        /// <returns>Task representing the navigation operation</returns>
        public async Task NavigateToMainApp()
        {
            Application.Current.MainPage = new AppShell();
            await Task.CompletedTask;
        }
    }
}
