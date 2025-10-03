using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Application.Interfaces.Services;
using Grocery.Domain.Entities;
using Grocery.Application.DTOs;
using System.Collections.ObjectModel;


namespace Grocery.App.ViewModels
{
    public partial class BoughtProductsViewModel : BaseViewModel
    {
        private readonly IBoughtProductsService _boughtProductsService;

        [ObservableProperty]
        Product selectedProduct;
        public ObservableCollection<BoughtProducts> BoughtProductsList { get; set; } = [];
        public ObservableCollection<Product> Products { get; set; }

        public BoughtProductsViewModel(IBoughtProductsService boughtProductsService, IProductService productService)
        {
            _boughtProductsService = boughtProductsService;
            Products = new(productService.GetAll());
        }

        partial void OnSelectedProductChanged(Product? oldValue, Product newValue)
        {
            if (newValue != null)
            {
                // Get bought products for the selected product
                List<BoughtProducts> boughtProducts = _boughtProductsService.Get(newValue.Id);
                
                // Clear and populate the observable collection
                BoughtProductsList.Clear();
                foreach (var boughtProduct in boughtProducts)
                {
                    BoughtProductsList.Add(boughtProduct);
                }
            }
            else
            {
                BoughtProductsList.Clear();
            }
        }

        [RelayCommand]
        public void NewSelectedProduct(Product product)
        {
            SelectedProduct = product;
        }
    }
}
