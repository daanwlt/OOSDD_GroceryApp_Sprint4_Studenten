
using Grocery.Domain.Entities;

namespace Grocery.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Client? Login(string email, string password);
    }
}
