
using Grocery.Application.Interfaces.Services;
using Grocery.Application.DTOs;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    public partial class BestSellingProductsViewModel : BaseViewModel
    {
        private readonly IGroceryListItemsService _groceryListItemsService;
        public ObservableCollection<BestSellingProducts> Products { get; set; } = [];
        public BestSellingProductsViewModel(IGroceryListItemsService groceryListItemsService)
        {
            _groceryListItemsService = groceryListItemsService;
            Products = [];
            Load();
        }

        public override void Load()
        {
            Products.Clear();
            foreach (BestSellingProducts item in _groceryListItemsService.GetBestSellingProducts())
            {
                Products.Add(item);
            }
        }

        public override void OnAppearing()
        {
            Load();
        }

        public override void OnDisappearing()
        {
            Products.Clear();
        }
    }
}
