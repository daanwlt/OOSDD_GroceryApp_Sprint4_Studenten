using Grocery.Application.Interfaces.Services;
using Grocery.Domain.Entities;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; set; }

        public ProductViewModel(IProductService productService)
        {
            Products = new(productService.GetAll());
        }

    }
}
