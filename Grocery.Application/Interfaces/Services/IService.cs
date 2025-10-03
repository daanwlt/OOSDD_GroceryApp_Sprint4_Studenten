using Grocery.Domain.Entities;

namespace Grocery.Application.Interfaces.Services
{
    public interface IService<T> where T : Model
    {
        List<T> GetAll();
        T? Get(int id);
        T Add(T item);
        T? Update(T item);
        T? Delete(T item);
    }
}
