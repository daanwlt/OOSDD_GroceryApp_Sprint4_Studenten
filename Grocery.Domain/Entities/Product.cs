using System.ComponentModel.DataAnnotations;

namespace Grocery.Domain.Entities
{
    /// <summary>
    /// Represents a product in the grocery application system.
    /// Contains product information including stock levels.
    /// Follows HBO-ICT coding guidelines for domain entities and business rules.
    /// Note: Pricing, categories, and expiration dates will be implemented in future sprints.
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

        // Note: Price, Category, and BestBeforeDate properties will be implemented in future sprints
        // according to the design documentation (UC12, UC14, UC15)

        /// <summary>
        /// Gets a value indicating whether the product is currently in stock.
        /// </summary>
        public bool IsInStock => Stock > 0;

        /// <summary>
        /// Gets a value indicating whether the product is out of stock.
        /// </summary>
        public bool IsOutOfStock => Stock == 0;

        // Note: Expiration-related properties will be implemented in future sprints
        // according to the design documentation (UC15)

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

        // Note: Price, category, and expiration-related methods will be implemented in future sprints
        // according to the design documentation (UC12, UC14, UC15)

        #endregion
    }
}
