using Grocery.Domain.Interfaces.Repositories;
using Grocery.Application.Interfaces.Services;
using Grocery.Domain.Entities;
using Grocery.Domain.ValueObjects;

namespace Grocery.Application.Services
{
    /// <summary>
    /// Service responsible for managing product operations and business logic.
    /// Provides comprehensive product management including categories, pricing, and expiration tracking.
    /// Follows HBO-ICT coding guidelines for service layer implementation.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        #region CRUD Operations

        /// <summary>
        /// Retrieves all products from the repository.
        /// </summary>
        /// <returns>A list of all products</returns>
        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        /// <summary>
        /// Retrieves a specific product by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the product</param>
        /// <returns>The product if found; otherwise, null</returns>
        public Product? Get(int id)
        {
            return _productRepository.Get(id);
        }

        /// <summary>
        /// Adds a new product to the repository.
        /// </summary>
        /// <param name="item">The product to add</param>
        /// <returns>The added product</returns>
        /// <exception cref="ArgumentException">Thrown when product data is invalid</exception>
        public Product Add(Product item)
        {
            // Validate product data according to business rules
            if (item == null)
                throw new ArgumentException("Product cannot be null", nameof(item));

            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Product name cannot be null or empty", nameof(item));

            if (item.Stock < 0)
                throw new ArgumentException("Product stock cannot be negative", nameof(item));

            if (item.Price <= 0)
                throw new ArgumentException("Product price must be greater than 0", nameof(item));

            return _productRepository.Add(item);
        }

        /// <summary>
        /// Updates an existing product in the repository.
        /// </summary>
        /// <param name="item">The product to update</param>
        /// <returns>The updated product if successful; otherwise, null</returns>
        public Product? Update(Product item)
        {
            if (item == null)
                throw new ArgumentException("Product cannot be null", nameof(item));

            return _productRepository.Update(item);
        }

        /// <summary>
        /// Deletes a product from the repository.
        /// </summary>
        /// <param name="item">The product to delete</param>
        /// <returns>The deleted product if successful; otherwise, null</returns>
        public Product? Delete(Product item)
        {
            if (item == null)
                throw new ArgumentException("Product cannot be null", nameof(item));

            return _productRepository.Delete(item);
        }

        #endregion

        #region Business Logic Methods

        /// <summary>
        /// Retrieves all products in a specific category.
        /// </summary>
        /// <param name="category">The product category to filter by</param>
        /// <returns>A list of products in the specified category</returns>
        public List<Product> GetProductsByCategory(ProductCategory category)
        {
            return _productRepository.GetAll()
                .Where(p => p.Category == category)
                .ToList();
        }

        /// <summary>
        /// Retrieves all products that are currently in stock.
        /// </summary>
        /// <returns>A list of products that are in stock</returns>
        public List<Product> GetInStockProducts()
        {
            return _productRepository.GetAll()
                .Where(p => p.IsInStock)
                .ToList();
        }

        /// <summary>
        /// Retrieves all products that are out of stock.
        /// </summary>
        /// <returns>A list of products that are out of stock</returns>
        public List<Product> GetOutOfStockProducts()
        {
            return _productRepository.GetAll()
                .Where(p => p.IsOutOfStock)
                .ToList();
        }

        /// <summary>
        /// Retrieves all products that are expired or expiring soon.
        /// </summary>
        /// <param name="daysAhead">The number of days to check ahead for expiration (default: 7)</param>
        /// <returns>A list of products that are expired or expiring soon</returns>
        public List<Product> GetExpiringProducts(int daysAhead = 7)
        {
            return _productRepository.GetAll()
                .Where(p => p.IsExpired || p.IsExpiringSoon(daysAhead))
                .ToList();
        }

        /// <summary>
        /// Retrieves all products within a specific price range.
        /// </summary>
        /// <param name="minPrice">The minimum price (inclusive)</param>
        /// <param name="maxPrice">The maximum price (inclusive)</param>
        /// <returns>A list of products within the specified price range</returns>
        public List<Product> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            if (minPrice < 0)
                throw new ArgumentException("Minimum price cannot be negative", nameof(minPrice));
            
            if (maxPrice < minPrice)
                throw new ArgumentException("Maximum price cannot be less than minimum price", nameof(maxPrice));

            return _productRepository.GetAll()
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .ToList();
        }

        /// <summary>
        /// Calculates the total value of all products in stock.
        /// </summary>
        /// <returns>The total value of all stock in euros</returns>
        public decimal GetTotalStockValue()
        {
            return _productRepository.GetAll()
                .Sum(p => p.GetStockValue());
        }

        /// <summary>
        /// Searches for products by name (case-insensitive).
        /// </summary>
        /// <param name="searchTerm">The term to search for in product names</param>
        /// <returns>A list of products matching the search term</returns>
        public List<Product> SearchProducts(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<Product>();

            return _productRepository.GetAll()
                .Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        #endregion
    }
}
