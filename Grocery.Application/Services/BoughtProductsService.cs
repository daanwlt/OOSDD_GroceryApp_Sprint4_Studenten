
using Grocery.Application.Interfaces.Services;
using Grocery.Application.DTOs;

namespace Grocery.Application.Services
{
    /// <summary>
    /// Service responsible for managing bought products operations.
    /// Follows Single Responsibility Principle by having only one responsibility:
    /// coordinating bought products data operations.
    /// </summary>
    public class BoughtProductsService : IBoughtProductsService
    {
        private readonly BoughtProductsAnalysisService _boughtProductsAnalysisService;

        public BoughtProductsService(BoughtProductsAnalysisService boughtProductsAnalysisService)
        {
            _boughtProductsAnalysisService = boughtProductsAnalysisService;
        }

        /// <summary>
        /// Gets bought products for a specific product by delegating to the analysis service.
        /// This method has a single responsibility: delegating to the appropriate service.
        /// </summary>
        /// <param name="productId">The product ID to get bought products for</param>
        /// <returns>List of bought products</returns>
        public List<BoughtProducts> Get(int? productId)
        {
            return _boughtProductsAnalysisService.GetBoughtProductsForProduct(productId);
        }
    }
}
