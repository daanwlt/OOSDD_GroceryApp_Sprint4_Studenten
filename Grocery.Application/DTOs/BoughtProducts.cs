
using Grocery.Domain.Entities;

namespace Grocery.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a bought product with associated client and grocery list information.
    /// Contains the relationship between a product purchase, the client who made the purchase, and the grocery list it was part of.
    /// Follows HBO-ICT coding guidelines for DTOs and data aggregation.
    /// </summary>
    public class BoughtProducts
    {
        #region Properties

        /// <summary>
        /// Gets or sets the product that was bought.
        /// This property contains the product details including name, stock, and other attributes.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the client who bought the product.
        /// This property contains the client information including name, email, and role.
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// Gets or sets the grocery list that contained this product purchase.
        /// This property contains the grocery list details including name, date, and color.
        /// </summary>
        public GroceryList GroceryList { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BoughtProducts class.
        /// </summary>
        /// <param name="client">The client who bought the product</param>
        /// <param name="groceryList">The grocery list that contained this product</param>
        /// <param name="product">The product that was bought</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null</exception>
        public BoughtProducts(Client client, GroceryList groceryList, Product product)
        {
            // Validate input parameters according to HBO-ICT guidelines
            Client = client ?? throw new ArgumentNullException(nameof(client));
            GroceryList = groceryList ?? throw new ArgumentNullException(nameof(groceryList));
            Product = product ?? throw new ArgumentNullException(nameof(product));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a formatted string representation of this bought product.
        /// </summary>
        /// <returns>A formatted string containing client, product, and grocery list information</returns>
        public string GetFormattedDescription()
        {
            return $"{Client.Name} bought {Product.Name} from list '{GroceryList.Name}' on {GroceryList.Date:yyyy-MM-dd}";
        }

        /// <summary>
        /// Determines if this purchase was made by an admin user.
        /// </summary>
        /// <returns>True if the client has admin role; otherwise, false</returns>
        public bool WasPurchasedByAdmin()
        {
            return Client.IsAdmin();
        }

        /// <summary>
        /// Gets the purchase date from the associated grocery list.
        /// </summary>
        /// <returns>The date when the purchase was made</returns>
        public DateOnly GetPurchaseDate()
        {
            return GroceryList.Date;
        }

        #endregion
    }
}
