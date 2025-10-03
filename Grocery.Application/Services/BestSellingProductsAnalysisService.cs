using Grocery.Domain.Interfaces.Repositories;
using Grocery.Domain.Entities;
using Grocery.Application.DTOs;

namespace Grocery.Application.Services
{
    /// <summary>
    /// Service responsible for analyzing and calculating best selling products.
    /// Follows Single Responsibility Principle by having only one responsibility:
    /// analyzing sales data and determining best selling products.
    /// </summary>
    public class BestSellingProductsAnalysisService
    {
        private readonly IGroceryListItemsRepository _groceryListItemsRepository;
        private readonly IProductRepository _productRepository;

        public BestSellingProductsAnalysisService(
            IGroceryListItemsRepository groceryListItemsRepository,
            IProductRepository productRepository)
        {
            _groceryListItemsRepository = groceryListItemsRepository;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Analyzes sales data and returns the top X best selling products.
        /// This method has a single responsibility: sales analysis and ranking.
        /// </summary>
        /// <param name="topX">Number of top products to return (default: 5)</param>
        /// <returns>List of best selling products with ranking</returns>
        public List<BestSellingProducts> GetBestSellingProducts(int topX = 5)
        {
            // Get all grocery list items for analysis
            List<GroceryListItem> allItems = _groceryListItemsRepository.GetAll();
            
            // Analyze sales data - group by product and count sales
            var productSales = AnalyzeProductSales(allItems);
            
            // Create ranked results
            return CreateRankedResults(productSales, topX);
        }

        /// <summary>
        /// Analyzes product sales data by grouping items by product ID and counting occurrences.
        /// This method has a single responsibility: sales data analysis.
        /// </summary>
        /// <param name="groceryListItems">All grocery list items to analyze</param>
        /// <returns>Ordered list of product sales data</returns>
        private List<(int ProductId, int SalesCount)> AnalyzeProductSales(List<GroceryListItem> groceryListItems)
        {
            return groceryListItems
                .GroupBy(item => item.ProductId)
                .Select(group => (ProductId: group.Key, SalesCount: group.Count()))
                .OrderByDescending(x => x.SalesCount)
                .ToList();
        }

        /// <summary>
        /// Creates ranked BestSellingProducts objects from sales analysis.
        /// This method has a single responsibility: creating ranked results.
        /// </summary>
        /// <param name="productSales">Analyzed sales data</param>
        /// <param name="topX">Number of top products to include</param>
        /// <returns>List of ranked best selling products</returns>
        private List<BestSellingProducts> CreateRankedResults(List<(int ProductId, int SalesCount)> productSales, int topX)
        {
            List<BestSellingProducts> bestSellingProducts = new();
            int ranking = 1;
            
            foreach (var (productId, salesCount) in productSales.Take(topX))
            {
                Product? product = _productRepository.Get(productId);
                if (product != null)
                {
                    bestSellingProducts.Add(new BestSellingProducts(
                        product.Id,
                        product.Name,
                        product.Stock,
                        salesCount,
                        ranking
                    ));
                    ranking++;
                }
            }
            
            return bestSellingProducts;
        }
    }
}
