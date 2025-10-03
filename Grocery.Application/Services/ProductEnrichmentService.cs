using Grocery.Domain.Interfaces.Repositories;
using Grocery.Domain.Entities;

namespace Grocery.Application.Services
{
    /// <summary>
    /// Service responsible for enriching grocery list items with product data.
    /// Follows Single Responsibility Principle by having only one responsibility:
    /// enriching entities with related product information.
    /// </summary>
    public class ProductEnrichmentService
    {
        private readonly IProductRepository _productRepository;

        public ProductEnrichmentService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Enriches a list of grocery list items with their corresponding product data.
        /// This method has a single responsibility: data enrichment.
        /// </summary>
        /// <param name="groceryListItems">The grocery list items to enrich</param>
        public void EnrichWithProductData(List<GroceryListItem> groceryListItems)
        {
            foreach (var item in groceryListItems)
            {
                item.Product = _productRepository.Get(item.ProductId) ?? new Product(0, "", 0);
            }
        }

        /// <summary>
        /// Enriches a single grocery list item with its corresponding product data.
        /// This method has a single responsibility: data enrichment for a single item.
        /// </summary>
        /// <param name="groceryListItem">The grocery list item to enrich</param>
        public void EnrichWithProductData(GroceryListItem groceryListItem)
        {
            groceryListItem.Product = _productRepository.Get(groceryListItem.ProductId) ?? new Product(0, "", 0);
        }
    }
}
