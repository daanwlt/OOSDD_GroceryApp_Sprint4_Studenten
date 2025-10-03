
using CommunityToolkit.Mvvm.ComponentModel;
using Grocery.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Grocery.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a best-selling product with sales analytics.
    /// Contains product information along with sales statistics and ranking.
    /// Follows HBO-ICT coding guidelines for DTOs and data presentation.
    /// </summary>
    public partial class BestSellingProducts : Model
    {
        #region Properties

        /// <summary>
        /// Gets or sets the current stock level of the product.
        /// This property represents the available inventory.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets the number of times this product has been sold.
        /// This property is observable and will notify UI when changed.
        /// </summary>
        [ObservableProperty]
        private int _nrOfSells;

        /// <summary>
        /// Gets or sets the ranking position of this product in the best-selling list.
        /// This property is observable and will notify UI when changed.
        /// </summary>
        [ObservableProperty]
        private int _ranking;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BestSellingProducts class.
        /// </summary>
        /// <param name="productId">The unique identifier of the product</param>
        /// <param name="name">The name of the product</param>
        /// <param name="stock">The current stock level of the product</param>
        /// <param name="nrOfSells">The number of times this product has been sold</param>
        /// <param name="ranking">The ranking position in the best-selling list</param>
        /// <exception cref="ArgumentException">Thrown when parameters are invalid</exception>
        public BestSellingProducts(int productId, string name, int stock, int nrOfSells, int ranking) : base(productId, name)
        {
            // Validate input parameters according to HBO-ICT guidelines
            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative", nameof(stock));
            
            if (nrOfSells < 0)
                throw new ArgumentException("Number of sells cannot be negative", nameof(nrOfSells));
            
            if (ranking <= 0)
                throw new ArgumentException("Ranking must be greater than 0", nameof(ranking));

            Stock = stock;
            NrOfSells = nrOfSells;
            Ranking = ranking;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Calculates the sales performance ratio (sells per stock unit).
        /// </summary>
        /// <returns>The sales performance ratio, or 0 if stock is 0</returns>
        public double GetSalesPerformanceRatio()
        {
            return Stock > 0 ? (double)NrOfSells / Stock : 0;
        }

        /// <summary>
        /// Determines if this product is a top performer based on sales count.
        /// </summary>
        /// <param name="threshold">The minimum number of sales to be considered a top performer</param>
        /// <returns>True if the product meets the threshold; otherwise, false</returns>
        public bool IsTopPerformer(int threshold = 10)
        {
            return NrOfSells >= threshold;
        }

        #endregion
    }
}
