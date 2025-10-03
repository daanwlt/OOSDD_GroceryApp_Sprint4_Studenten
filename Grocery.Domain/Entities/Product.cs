using System.ComponentModel.DataAnnotations;
using Grocery.Domain.ValueObjects;

namespace Grocery.Domain.Entities
{
    /// <summary>
    /// Represents a product in the grocery application system.
    /// Contains product information including stock levels, pricing, categories, and expiration dates.
    /// Follows HBO-ICT coding guidelines for domain entities and business rules.
    /// </summary>
    public class Product : Model
    {
        #region Properties

        /// <summary>
        /// Gets or sets the current stock level of the product.
        /// Stock cannot be negative according to business rules.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets the price of the product in euros.
        /// Price must be positive according to business rules.
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the category of the product.
        /// This property determines how the product is classified in the system.
        /// </summary>
        public ProductCategory Category { get; set; } = ProductCategory.Other;

        /// <summary>
        /// Gets or sets the best before date (THT - Tenminste Houdbaar Tot) of the product.
        /// This property is optional and can be null for non-perishable items.
        /// </summary>
        public DateOnly? BestBeforeDate { get; set; }

        /// <summary>
        /// Gets a value indicating whether the product is currently in stock.
        /// </summary>
        public bool IsInStock => Stock > 0;

        /// <summary>
        /// Gets a value indicating whether the product is out of stock.
        /// </summary>
        public bool IsOutOfStock => Stock == 0;

        /// <summary>
        /// Gets a value indicating whether the product is expired based on the best before date.
        /// </summary>
        public bool IsExpired => BestBeforeDate.HasValue && BestBeforeDate.Value < DateOnly.FromDateTime(DateTime.Today);

        /// <summary>
        /// Gets a value indicating whether the product will expire within the specified number of days.
        /// </summary>
        /// <param name="days">The number of days to check ahead</param>
        /// <returns>True if the product will expire within the specified days; otherwise, false</returns>
        public bool IsExpiringSoon(int days = 7)
        {
            if (!BestBeforeDate.HasValue) return false;
            var expirationThreshold = DateOnly.FromDateTime(DateTime.Today.AddDays(days));
            return BestBeforeDate.Value <= expirationThreshold;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Product class with basic information.
        /// </summary>
        /// <param name="id">The unique identifier for the product</param>
        /// <param name="name">The name of the product</param>
        /// <param name="stock">The initial stock level of the product</param>
        /// <exception cref="ArgumentException">Thrown when stock is negative</exception>
        public Product(int id, string name, int stock) : base(id, name)
        {
            // Validate business rules according to HBO-ICT guidelines
            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative", nameof(stock));

            Stock = stock;
            Price = 0; // Default price
            Category = ProductCategory.Other; // Default category
            BestBeforeDate = null; // No expiration date by default
        }

        /// <summary>
        /// Initializes a new instance of the Product class with complete information.
        /// </summary>
        /// <param name="id">The unique identifier for the product</param>
        /// <param name="name">The name of the product</param>
        /// <param name="stock">The initial stock level of the product</param>
        /// <param name="price">The price of the product in euros</param>
        /// <param name="category">The category of the product</param>
        /// <param name="bestBeforeDate">The best before date of the product (optional)</param>
        /// <exception cref="ArgumentException">Thrown when parameters are invalid</exception>
        public Product(int id, string name, int stock, decimal price, ProductCategory category, DateOnly? bestBeforeDate = null) : base(id, name)
        {
            // Validate business rules according to HBO-ICT guidelines
            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative", nameof(stock));
            
            if (price <= 0)
                throw new ArgumentException("Price must be greater than 0", nameof(price));

            Stock = stock;
            Price = price;
            Category = category;
            BestBeforeDate = bestBeforeDate;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Increases the stock level by the specified amount.
        /// </summary>
        /// <param name="amount">The amount to add to the stock</param>
        /// <exception cref="ArgumentException">Thrown when amount is negative</exception>
        public void IncreaseStock(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative", nameof(amount));

            Stock += amount;
        }

        /// <summary>
        /// Decreases the stock level by the specified amount.
        /// </summary>
        /// <param name="amount">The amount to subtract from the stock</param>
        /// <exception cref="ArgumentException">Thrown when amount is negative or exceeds current stock</exception>
        public void DecreaseStock(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative", nameof(amount));
            
            if (amount > Stock)
                throw new ArgumentException("Amount cannot exceed current stock", nameof(amount));

            Stock -= amount;
        }

        /// <summary>
        /// Checks if the product has sufficient stock for the requested amount.
        /// </summary>
        /// <param name="requestedAmount">The amount of stock requested</param>
        /// <returns>True if sufficient stock is available; otherwise, false</returns>
        public bool HasSufficientStock(int requestedAmount)
        {
            return requestedAmount > 0 && Stock >= requestedAmount;
        }

        /// <summary>
        /// Updates the price of the product.
        /// </summary>
        /// <param name="newPrice">The new price for the product</param>
        /// <exception cref="ArgumentException">Thrown when price is not positive</exception>
        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("Price must be greater than 0", nameof(newPrice));

            Price = newPrice;
        }

        /// <summary>
        /// Updates the category of the product.
        /// </summary>
        /// <param name="newCategory">The new category for the product</param>
        public void UpdateCategory(ProductCategory newCategory)
        {
            Category = newCategory;
        }

        /// <summary>
        /// Updates the best before date of the product.
        /// </summary>
        /// <param name="newBestBeforeDate">The new best before date (can be null for non-perishable items)</param>
        public void UpdateBestBeforeDate(DateOnly? newBestBeforeDate)
        {
            BestBeforeDate = newBestBeforeDate;
        }

        /// <summary>
        /// Calculates the total value of the current stock.
        /// </summary>
        /// <returns>The total value of the stock in euros</returns>
        public decimal GetStockValue()
        {
            return Stock * Price;
        }

        /// <summary>
        /// Determines if the product belongs to a specific category.
        /// </summary>
        /// <param name="category">The category to check</param>
        /// <returns>True if the product belongs to the specified category; otherwise, false</returns>
        public bool BelongsToCategory(ProductCategory category)
        {
            return Category == category;
        }

        /// <summary>
        /// Gets a formatted string representation of the product's expiration status.
        /// </summary>
        /// <returns>A formatted string describing the expiration status</returns>
        public string GetExpirationStatus()
        {
            if (!BestBeforeDate.HasValue)
                return "Geen vervaldatum";

            if (IsExpired)
                return $"Verlopen op {BestBeforeDate.Value:dd-MM-yyyy}";

            if (IsExpiringSoon(3))
                return $"Verloopt binnenkort op {BestBeforeDate.Value:dd-MM-yyyy}";

            return $"Houdbaar tot {BestBeforeDate.Value:dd-MM-yyyy}";
        }

        #endregion
    }
}
