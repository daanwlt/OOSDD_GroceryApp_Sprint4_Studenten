
using Grocery.Application.DTOs;

namespace Grocery.Application.Interfaces.Services
{
    public interface IBoughtProductsService
    {
        public List<BoughtProducts> Get(int? productId);
    }
}
