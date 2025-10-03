using Grocery.Domain.Interfaces.Repositories;
using Grocery.Application.Interfaces.Services;
using Grocery.Domain.Entities;
using Grocery.Application.DTOs;

namespace Grocery.Application.Services
{
    /// <summary>
    /// Service responsible for managing grocery list items CRUD operations.
    /// Follows Single Responsibility Principle by having only one responsibility:
    /// managing grocery list items data operations.
    /// </summary>
    public class GroceryListItemsService : IGroceryListItemsService
    {
        private readonly IGroceryListItemsRepository _groceriesRepository;
        private readonly ProductEnrichmentService _productEnrichmentService;
        private readonly BestSellingProductsAnalysisService _bestSellingProductsAnalysisService;

        public GroceryListItemsService(
            IGroceryListItemsRepository groceriesRepository, 
            ProductEnrichmentService productEnrichmentService,
            BestSellingProductsAnalysisService bestSellingProductsAnalysisService)
        {
            _groceriesRepository = groceriesRepository;
            _productEnrichmentService = productEnrichmentService;
            _bestSellingProductsAnalysisService = bestSellingProductsAnalysisService;
        }

        /// <summary>
        /// Gets all grocery list items with enriched product data.
        /// This method has a single responsibility: data retrieval and enrichment.
        /// </summary>
        /// <returns>List of grocery list items with product data</returns>
        public List<GroceryListItem> GetAll()
        {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll();
            _productEnrichmentService.EnrichWithProductData(groceryListItems);
            return groceryListItems;
        }

        /// <summary>
        /// Gets grocery list items for a specific grocery list with enriched product data.
        /// This method has a single responsibility: filtered data retrieval and enrichment.
        /// </summary>
        /// <param name="groceryListId">The grocery list ID to filter by</param>
        /// <returns>List of grocery list items for the specified list with product data</returns>
        public List<GroceryListItem> GetAllOnGroceryListId(int groceryListId)
        {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll()
                .Where(g => g.GroceryListId == groceryListId)
                .ToList();
            _productEnrichmentService.EnrichWithProductData(groceryListItems);
            return groceryListItems;
        }

        public GroceryListItem Add(GroceryListItem item)
        {
            return _groceriesRepository.Add(item);
        }

        public GroceryListItem? Delete(GroceryListItem item)
        {
            throw new NotImplementedException();
        }

        public GroceryListItem? Get(int id)
        {
            return _groceriesRepository.Get(id);
        }

        public GroceryListItem? Update(GroceryListItem item)
        {
            return _groceriesRepository.Update(item);
        }

        /// <summary>
        /// Gets best selling products by delegating to the analysis service.
        /// This method has a single responsibility: delegating to the appropriate service.
        /// </summary>
        /// <param name="topX">Number of top products to return</param>
        /// <returns>List of best selling products</returns>
        public List<BestSellingProducts> GetBestSellingProducts(int topX = 5)
        {
            return _bestSellingProductsAnalysisService.GetBestSellingProducts(topX);
        }
    }
}
