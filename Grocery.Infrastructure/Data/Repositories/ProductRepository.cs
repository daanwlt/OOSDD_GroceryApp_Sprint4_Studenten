using Grocery.Domain.Interfaces.Repositories;
using Grocery.Domain.Entities;
using Grocery.Domain.ValueObjects;

namespace Grocery.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repository implementation for Product entities.
    /// Provides data access operations for products including categories, prices, and expiration dates.
    /// Follows HBO-ICT coding guidelines for repository pattern implementation.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> products;
        
        public ProductRepository()
        {
            // Initialize products with basic information (only stock levels according to current design)
            products = [
                new Product(1, "Melk", 300),
                new Product(2, "Kaas", 100),
                new Product(3, "Brood", 400),
                new Product(4, "Cornflakes", 0),
                new Product(5, "Appels", 150),
                new Product(6, "Kipfilet", 50),
                new Product(7, "Cola", 200),
                new Product(8, "IJs", 25),
                new Product(9, "Shampoo", 75),
                new Product(10, "Chips", 120)
            ];
        }
        public List<Product> GetAll()
        {
            return products;
        }

        public Product? Get(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Adds a new product to the repository.
        /// </summary>
        /// <param name="item">The product to add</param>
        /// <returns>The added product</returns>
        /// <exception cref="ArgumentException">Thrown when product ID already exists</exception>
        public Product Add(Product item)
        {
            if (item == null)
                throw new ArgumentException("Product cannot be null", nameof(item));

            // Check if product with same ID already exists
            if (products.Any(p => p.Id == item.Id))
                throw new ArgumentException($"Product with ID {item.Id} already exists", nameof(item));

            products.Add(item);
            return item;
        }

        /// <summary>
        /// Deletes a product from the repository.
        /// </summary>
        /// <param name="item">The product to delete</param>
        /// <returns>The deleted product if found; otherwise, null</returns>
        public Product? Delete(Product item)
        {
            if (item == null)
                return null;

            var productToDelete = products.FirstOrDefault(p => p.Id == item.Id);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
                return productToDelete;
            }

            return null;
        }

        /// <summary>
        /// Updates an existing product in the repository.
        /// </summary>
        /// <param name="item">The product to update</param>
        /// <returns>The updated product if found; otherwise, null</returns>
        public Product? Update(Product item)
        {
            if (item == null)
                return null;

            var existingProduct = products.FirstOrDefault(p => p.Id == item.Id);
            if (existingProduct == null)
                return null;

            // Update all properties of the existing product
            existingProduct.Name = item.Name;
            existingProduct.Stock = item.Stock;

            return existingProduct;
        }
    }
}
