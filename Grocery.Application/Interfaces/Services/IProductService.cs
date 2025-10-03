using Grocery.Domain.Entities;
using Grocery.Domain.ValueObjects;

namespace Grocery.Application.Interfaces.Services
{
    /// <summary>
    /// Interface for product service operations.
    /// Defines the contract for product management including categories, pricing, and expiration tracking.
    /// Follows HBO-ICT coding guidelines for interface design and service contracts.
    /// </summary>
    public interface IProductService
    {
        #region CRUD Operations

        /// <summary>
        /// Retrieves all products from the repository.
        /// </summary>
        /// <returns>A list of all products</returns>
        List<Product> GetAll();

        /// <summary>
        /// Retrieves a specific product by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the product</param>
        /// <returns>The product if found; otherwise, null</returns>
        Product? Get(int id);

        /// <summary>
        /// Adds a new product to the repository.
        /// </summary>
        /// <param name="item">The product to add</param>
        /// <returns>The added product</returns>
        Product Add(Product item);

        /// <summary>
        /// Updates an existing product in the repository.
        /// </summary>
        /// <param name="item">The product to update</param>
        /// <returns>The updated product if successful; otherwise, null</returns>
        Product? Update(Product item);

        /// <summary>
        /// Deletes a product from the repository.
        /// </summary>
        /// <param name="item">The product to delete</param>
        /// <returns>The deleted product if successful; otherwise, null</returns>
        Product? Delete(Product item);

        #endregion

        #region Business Logic Methods

        /// <summary>
        /// Retrieves all products in a specific category.
        /// </summary>
        /// <param name="category">The product category to filter by</param>
        /// <returns>A list of products in the specified category</returns>
        List<Product> GetProductsByCategory(ProductCategory category);

        /// <summary>
        /// Retrieves all products that are currently in stock.
        /// </summary>
        /// <returns>A list of products that are in stock</returns>
        List<Product> GetInStockProducts();

        /// <summary>
        /// Retrieves all products that are out of stock.
        /// </summary>
        /// <returns>A list of products that are out of stock</returns>
        List<Product> GetOutOfStockProducts();

        /// <summary>
        /// Retrieves all products that are expired or expiring soon.
        /// </summary>
        /// <param name="daysAhead">The number of days to check ahead for expiration (default: 7)</param>
        /// <returns>A list of products that are expired or expiring soon</returns>
        List<Product> GetExpiringProducts(int daysAhead = 7);

        /// <summary>
        /// Retrieves all products within a specific price range.
        /// </summary>
        /// <param name="minPrice">The minimum price (inclusive)</param>
        /// <param name="maxPrice">The maximum price (inclusive)</param>
        /// <returns>A list of products within the specified price range</returns>
        List<Product> GetProductsByPriceRange(decimal minPrice, decimal maxPrice);

        /// <summary>
        /// Calculates the total value of all products in stock.
        /// </summary>
        /// <returns>The total value of all stock in euros</returns>
        decimal GetTotalStockValue();

        /// <summary>
        /// Searches for products by name (case-insensitive).
        /// </summary>
        /// <param name="searchTerm">The term to search for in product names</param>
        /// <returns>A list of products matching the search term</returns>
        List<Product> SearchProducts(string searchTerm);

        #endregion
    }
}
