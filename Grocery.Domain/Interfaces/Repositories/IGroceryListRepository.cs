using Grocery.Domain.Entities;

namespace Grocery.Domain.Interfaces.Repositories
{
    public interface IGroceryListRepository
    {
        public List<GroceryList> GetAll();
        public GroceryList Add(GroceryList item);

        public GroceryList? Delete(GroceryList item);

        public GroceryList? Get(int id);

        public GroceryList? Update(GroceryList item);
    }
}
