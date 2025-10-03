using Grocery.Domain.Interfaces.Repositories;
using Grocery.Application.Interfaces.Services;
using Grocery.Domain.Entities;

namespace Grocery.Application.Services
{
    /// <summary>
    /// Service responsible for managing product operations and business logic.
    /// Provides basic product management including stock operations.
    /// Follows HBO-ICT coding guidelines for service layer implementation.
    /// Note: Category, pricing, and expiration features will be implemented in future sprints.
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
        /// Retrieves a product by its unique identifier.
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
        public Product Add(Product item)
        {
            return _productRepository.Add(item);
        }

        /// <summary>
        /// Updates an existing product in the repository.
        /// </summary>
        /// <param name="item">The product to update</param>
        /// <returns>The updated product if found; otherwise, null</returns>
        public Product? Update(Product item)
        {
            return _productRepository.Update(item);
        }

        /// <summary>
        /// Deletes a product from the repository.
        /// </summary>
        /// <param name="item">The product to delete</param>
        /// <returns>The deleted product if found; otherwise, null</returns>
        public Product? Delete(Product item)
        {
            return _productRepository.Delete(item);
        }

        #endregion

        #region Business Logic Methods

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

        // Note: Additional business logic methods can be implemented as needed

        #endregion
    }
}