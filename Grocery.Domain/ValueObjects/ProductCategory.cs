namespace Grocery.Domain.ValueObjects
{
    /// <summary>
    /// Represents the category of a product in the grocery application system.
    /// Defines the different product categories available in the system.
    /// Follows HBO-ICT coding guidelines for value objects and enums.
    /// </summary>
    public enum ProductCategory
    {
        /// <summary>
        /// Dairy products including milk, cheese, yogurt, etc.
        /// </summary>
        Dairy = 0,

        /// <summary>
        /// Meat and poultry products.
        /// </summary>
        Meat = 1,

        /// <summary>
        /// Fresh fruits and vegetables.
        /// </summary>
        Produce = 2,

        /// <summary>
        /// Bread, cereals, and grain products.
        /// </summary>
        Bakery = 3,

        /// <summary>
        /// Frozen foods and ice cream.
        /// </summary>
        Frozen = 4,

        /// <summary>
        /// Canned and packaged goods.
        /// </summary>
        Pantry = 5,

        /// <summary>
        /// Beverages including soft drinks, juices, and water.
        /// </summary>
        Beverages = 6,

        /// <summary>
        /// Snacks, chips, and confectionery.
        /// </summary>
        Snacks = 7,

        /// <summary>
        /// Household and cleaning products.
        /// </summary>
        Household = 8,

        /// <summary>
        /// Personal care and hygiene products.
        /// </summary>
        PersonalCare = 9,

        /// <summary>
        /// Other products that don't fit into specific categories.
        /// </summary>
        Other = 10
    }
}
