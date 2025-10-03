using Grocery.Domain.Interfaces.Repositories;
using Grocery.Application.Interfaces.Services;
using Grocery.Domain.Entities;

namespace Grocery.Application.Services
{
    public class GroceryListService : IGroceryListService
    {
        private readonly IGroceryListRepository _groceryRepository;
        public GroceryListService(IGroceryListRepository groceryRepository)
        {
            _groceryRepository = groceryRepository;
        }
        public List<GroceryList> GetAll()
        {
            return _groceryRepository.GetAll();
        }
        public GroceryList Add(GroceryList item)
        {
            return _groceryRepository.Add(item);
        }

        public GroceryList? Delete(GroceryList item)
        {
            return _groceryRepository.Delete(item);
        }

        public GroceryList? Get(int id)
        {
            return _groceryRepository.Get(id);
        }

        public GroceryList? Update(GroceryList item)
        {
            return _groceryRepository.Update(item);
        }
    }
}
