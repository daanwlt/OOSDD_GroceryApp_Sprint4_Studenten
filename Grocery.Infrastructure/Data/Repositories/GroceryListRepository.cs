using Grocery.Domain.Interfaces.Repositories;
using Grocery.Domain.Entities;

namespace Grocery.Infrastructure.Data.Repositories
{
    public class GroceryListRepository : IGroceryListRepository
    {
        private readonly List<GroceryList> groceryLists;

        public GroceryListRepository()
        {
            groceryLists = [
                new GroceryList(1, "Boodschappen familieweekend", DateOnly.Parse("2024-12-14"), "#FF6A00", 1),
                new GroceryList(2, "Kerstboodschappen", DateOnly.Parse("2024-12-07"), "#626262", 1),
                new GroceryList(3, "Weekend boodschappen", DateOnly.Parse("2024-11-30"), "#003300", 1)];
        }

        public List<GroceryList> GetAll()
        {
            return groceryLists;
        }
        /// <summary>
        /// Adds a new grocery list to the repository.
        /// </summary>
        /// <param name="item">The grocery list to add</param>
        /// <returns>The added grocery list with generated ID</returns>
        /// <exception cref="ArgumentException">Thrown when item is null or invalid</exception>
        public GroceryList Add(GroceryList item)
        {
            if (item == null)
                throw new ArgumentException("Item cannot be null", nameof(item));

            // Generate new ID safely
            int newId = groceryLists.Count > 0 ? groceryLists.Max(g => g.Id) + 1 : 1;
            
            // Create new item with generated ID
            var newItem = new GroceryList(newId, item.Name, item.Date, item.Color, item.ClientId);
            groceryLists.Add(newItem);
            
            return newItem;
        }

        /// <summary>
        /// Deletes a grocery list from the repository.
        /// </summary>
        /// <param name="item">The grocery list to delete</param>
        /// <returns>The deleted grocery list if found; otherwise, null</returns>
        public GroceryList? Delete(GroceryList item)
        {
            if (item == null)
                return null;

            var itemToDelete = groceryLists.FirstOrDefault(g => g.Id == item.Id);
            if (itemToDelete != null)
            {
                groceryLists.Remove(itemToDelete);
                return itemToDelete;
            }

            return null;
        }

        public GroceryList? Get(int id)
        {
            GroceryList? groceryList = groceryLists.FirstOrDefault(g => g.Id == id);
            return groceryList;
        }

        /// <summary>
        /// Updates an existing grocery list in the repository.
        /// </summary>
        /// <param name="item">The grocery list to update</param>
        /// <returns>The updated grocery list if found; otherwise, null</returns>
        public GroceryList? Update(GroceryList item)
        {
            if (item == null)
                return null;

            var existingItem = groceryLists.FirstOrDefault(g => g.Id == item.Id);
            if (existingItem == null)
                return null;

            // Update all properties of the existing item
            existingItem.Name = item.Name;
            existingItem.Date = item.Date;
            existingItem.Color = item.Color;
            existingItem.ClientId = item.ClientId;

            return existingItem;
        }
    }
}
