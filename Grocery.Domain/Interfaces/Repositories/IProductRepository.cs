using Grocery.Domain.Entities;

namespace Grocery.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        public List<Product> GetAll();

        public Product? Get(int id);

        public Product Add(Product item);

        public Product? Delete(Product item);

        public Product? Update(Product item);
    }
}
