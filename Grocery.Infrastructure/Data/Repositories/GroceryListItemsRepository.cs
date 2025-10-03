using Grocery.Domain.Interfaces.Repositories;
using Grocery.Domain.Entities;

namespace Grocery.Infrastructure.Data.Repositories
{
    public class GroceryListItemsRepository : IGroceryListItemsRepository
    {
        private readonly List<GroceryListItem> groceryListItems;

        public GroceryListItemsRepository()
        {
            groceryListItems = [
                new GroceryListItem(1, 1, 1, 3),
                new GroceryListItem(2, 1, 2, 1),
                new GroceryListItem(3, 1, 3, 4),
                new GroceryListItem(4, 2, 1, 2),
                new GroceryListItem(5, 2, 2, 5),
            ];
        }

        public List<GroceryListItem> GetAll()
        {
            return groceryListItems;
        }

        public List<GroceryListItem> GetAllOnGroceryListId(int id)
        {
            return groceryListItems.Where(g => g.GroceryListId == id).ToList();
        }

        /// <summary>
        /// Adds a new grocery list item to the repository.
        /// </summary>
        /// <param name="item">The grocery list item to add</param>
        /// <returns>The added grocery list item with generated ID</returns>
        /// <exception cref="ArgumentException">Thrown when item is null or invalid</exception>
        public GroceryListItem Add(GroceryListItem item)
        {
            if (item == null)
                throw new ArgumentException("Item cannot be null", nameof(item));

            // Generate new ID safely
            int newId = groceryListItems.Count > 0 ? groceryListItems.Max(g => g.Id) + 1 : 1;
            
            // Create new item with generated ID (don't modify input parameter)
            var newItem = new GroceryListItem(newId, item.GroceryListId, item.ProductId, item.Amount);
            groceryListItems.Add(newItem);
            
            return newItem;
        }

        /// <summary>
        /// Deletes a grocery list item from the repository.
        /// </summary>
        /// <param name="item">The grocery list item to delete</param>
        /// <returns>The deleted grocery list item if found; otherwise, null</returns>
        public GroceryListItem? Delete(GroceryListItem item)
        {
            if (item == null)
                return null;

            var itemToDelete = groceryListItems.FirstOrDefault(i => i.Id == item.Id);
            if (itemToDelete != null)
            {
                groceryListItems.Remove(itemToDelete);
                return itemToDelete;
            }

            return null;
        }

        public GroceryListItem? Get(int id)
        {
            return groceryListItems.FirstOrDefault(g => g.Id == id);
        }

        /// <summary>
        /// Updates an existing grocery list item in the repository.
        /// </summary>
        /// <param name="item">The grocery list item to update</param>
        /// <returns>The updated grocery list item if found; otherwise, null</returns>
        public GroceryListItem? Update(GroceryListItem item)
        {
            if (item == null)
                return null;

            var existingItem = groceryListItems.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem == null)
                return null;

            // Update all properties of the existing item
            existingItem.GroceryListId = item.GroceryListId;
            existingItem.ProductId = item.ProductId;
            existingItem.Amount = item.Amount;

            return existingItem;
        }
    }
}
