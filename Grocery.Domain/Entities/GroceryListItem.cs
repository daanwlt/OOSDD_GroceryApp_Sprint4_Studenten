using System.ComponentModel.DataAnnotations;

namespace Grocery.Domain.Entities
{
    /// <summary>
    /// Represents an item in a grocery list.
    /// Contains the relationship between a grocery list and a product with quantity information.
    /// Follows HBO-ICT coding guidelines for domain entities and business rules.
    /// </summary>
    public class GroceryListItem : Model
    {
        #region Properties

        /// <summary>
        /// Gets or sets the identifier of the grocery list this item belongs to.
        /// This property is required and must reference a valid grocery list.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Grocery List ID must be greater than 0")]
        public int GroceryListId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the product this item represents.
        /// This property is required and must reference a valid product.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Product ID must be greater than 0")]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity/amount of the product in this list item.
        /// Amount must be positive according to business rules.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the product associated with this list item.
        /// This property is populated by the ProductEnrichmentService.
        /// </summary>
        public Product Product { get; set; } = new(0, "None", 0);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GroceryListItem class.
        /// </summary>
        /// <param name="id">The unique identifier for the grocery list item</param>
        /// <param name="groceryListId">The identifier of the grocery list this item belongs to</param>
        /// <param name="productId">The identifier of the product this item represents</param>
        /// <param name="amount">The quantity/amount of the product</param>
        /// <exception cref="ArgumentException">Thrown when required parameters are invalid</exception>
        public GroceryListItem(int id, int groceryListId, int productId, int amount) : base(id, "Grocery List Item")
        {
            // Validate input parameters according to HBO-ICT guidelines
            if (groceryListId <= 0)
                throw new ArgumentException("Grocery List ID must be greater than 0", nameof(groceryListId));
            
            if (productId <= 0)
                throw new ArgumentException("Product ID must be greater than 0", nameof(productId));
            
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than 0", nameof(amount));

            GroceryListId = groceryListId;
            ProductId = productId;
            Amount = amount;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the amount of this grocery list item.
        /// </summary>
        /// <param name="newAmount">The new amount for this item</param>
        /// <exception cref="ArgumentException">Thrown when amount is not positive</exception>
        public void UpdateAmount(int newAmount)
        {
            if (newAmount <= 0)
                throw new ArgumentException("Amount must be greater than 0", nameof(newAmount));

            Amount = newAmount;
        }

        /// <summary>
        /// Determines if this item belongs to the specified grocery list.
        /// </summary>
        /// <param name="groceryListId">The grocery list ID to check</param>
        /// <returns>True if the item belongs to the list; otherwise, false</returns>
        public bool BelongsToGroceryList(int groceryListId)
        {
            return GroceryListId == groceryListId;
        }

        /// <summary>
        /// Determines if this item represents the specified product.
        /// </summary>
        /// <param name="productId">The product ID to check</param>
        /// <returns>True if the item represents the product; otherwise, false</returns>
        public bool RepresentsProduct(int productId)
        {
            return ProductId == productId;
        }

        #endregion
    }
}
