using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Grocery.Domain.Entities
{
    /// <summary>
    /// Represents a grocery list in the application system.
    /// Contains list information including creation date, color, and associated client.
    /// Follows HBO-ICT coding guidelines for domain entities and data validation.
    /// </summary>
    public partial class GroceryList : Model
    {
        #region Properties

        /// <summary>
        /// Gets or sets the creation date of the grocery list.
        /// This property is required and represents when the list was created.
        /// </summary>
        [Required]
        public DateOnly Date { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the client who owns this grocery list.
        /// This property is required and must reference a valid client.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Client ID must be greater than 0")]
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the color theme of the grocery list.
        /// This property is observable and will notify UI when changed.
        /// </summary>
        [ObservableProperty]
        private string _color = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GroceryList class.
        /// </summary>
        /// <param name="id">The unique identifier for the grocery list</param>
        /// <param name="name">The name of the grocery list</param>
        /// <param name="date">The creation date of the grocery list</param>
        /// <param name="color">The color theme of the grocery list</param>
        /// <param name="clientId">The identifier of the client who owns this list</param>
        /// <exception cref="ArgumentException">Thrown when required parameters are invalid</exception>
        public GroceryList(int id, string name, DateOnly date, string color, int clientId) : base(id, name)
        {
            // Validate input parameters according to HBO-ICT guidelines
            if (string.IsNullOrWhiteSpace(color))
                throw new ArgumentException("Color cannot be null or empty", nameof(color));
            
            if (clientId <= 0)
                throw new ArgumentException("Client ID must be greater than 0", nameof(clientId));

            Date = date;
            Color = color;
            ClientId = clientId;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines if this grocery list belongs to the specified client.
        /// </summary>
        /// <param name="clientId">The client ID to check</param>
        /// <returns>True if the list belongs to the client; otherwise, false</returns>
        public bool BelongsToClient(int clientId)
        {
            return ClientId == clientId;
        }

        /// <summary>
        /// Updates the color theme of the grocery list.
        /// </summary>
        /// <param name="newColor">The new color theme</param>
        /// <exception cref="ArgumentException">Thrown when color is null or empty</exception>
        public void UpdateColor(string newColor)
        {
            if (string.IsNullOrWhiteSpace(newColor))
                throw new ArgumentException("Color cannot be null or empty", nameof(newColor));

            Color = newColor;
        }

        #endregion
    }
}
