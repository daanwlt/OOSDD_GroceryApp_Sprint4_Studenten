using Grocery.Domain.Interfaces.Repositories;
using Grocery.Domain.Entities;
using Grocery.Application.DTOs;

namespace Grocery.Application.Services
{
    /// <summary>
    /// Service responsible for analyzing bought products data.
    /// Follows Single Responsibility Principle by having only one responsibility:
    /// analyzing and aggregating bought products information.
    /// </summary>
    public class BoughtProductsAnalysisService
    {
        private readonly IGroceryListItemsRepository _groceryListItemsRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGroceryListRepository _groceryListRepository;

        public BoughtProductsAnalysisService(
            IGroceryListItemsRepository groceryListItemsRepository,
            IGroceryListRepository groceryListRepository,
            IClientRepository clientRepository,
            IProductRepository productRepository)
        {
            _groceryListItemsRepository = groceryListItemsRepository;
            _groceryListRepository = groceryListRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Analyzes bought products for a specific product ID.
        /// This method has a single responsibility: bought products analysis.
        /// </summary>
        /// <param name="productId">The product ID to analyze</param>
        /// <returns>List of bought products with client and grocery list information</returns>
        public List<BoughtProducts> GetBoughtProductsForProduct(int? productId)
        {
            if (productId == null)
                return new List<BoughtProducts>();

            // Get relevant grocery list items
            List<GroceryListItem> relevantItems = GetRelevantGroceryListItems(productId.Value);
            
            // Aggregate data for each item
            return AggregateBoughtProductsData(relevantItems);
        }

        /// <summary>
        /// Gets grocery list items that contain the specified product.
        /// This method has a single responsibility: data retrieval.
        /// </summary>
        /// <param name="productId">The product ID to search for</param>
        /// <returns>List of relevant grocery list items</returns>
        private List<GroceryListItem> GetRelevantGroceryListItems(int productId)
        {
            return _groceryListItemsRepository.GetAll()
                .Where(item => item.ProductId == productId)
                .ToList();
        }

        /// <summary>
        /// Aggregates data from grocery list items into BoughtProducts objects.
        /// This method has a single responsibility: data aggregation.
        /// </summary>
        /// <param name="groceryListItems">Grocery list items to aggregate</param>
        /// <returns>List of aggregated bought products</returns>
        private List<BoughtProducts> AggregateBoughtProductsData(List<GroceryListItem> groceryListItems)
        {
            List<BoughtProducts> boughtProducts = new();

            foreach (var item in groceryListItems)
            {
                var boughtProduct = CreateBoughtProductFromItem(item);
                if (boughtProduct != null)
                {
                    boughtProducts.Add(boughtProduct);
                }
            }

            return boughtProducts;
        }

        /// <summary>
        /// Creates a BoughtProducts object from a grocery list item.
        /// This method has a single responsibility: object creation.
        /// </summary>
        /// <param name="item">The grocery list item to convert</param>
        /// <returns>BoughtProducts object or null if data is incomplete</returns>
        private BoughtProducts? CreateBoughtProductFromItem(GroceryListItem item)
        {
            // Get the grocery list to find the client
            GroceryList? groceryList = _groceryListRepository.Get(item.GroceryListId);
            if (groceryList == null) return null;

            // Get the client who owns this grocery list
            Client? client = _clientRepository.Get(groceryList.ClientId);
            if (client == null) return null;

            // Get the product
            Product? product = _productRepository.Get(item.ProductId);
            if (product == null) return null;

            return new BoughtProducts(client, groceryList, product);
        }
    }
}
