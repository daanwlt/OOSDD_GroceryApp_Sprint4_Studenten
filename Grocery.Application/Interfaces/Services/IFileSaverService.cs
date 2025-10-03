
namespace Grocery.Application.Interfaces.Services
{
    public interface IFileSaverService
    {
        Task SaveFileAsync(string fileName, string content, CancellationToken cancellationToken);
    }
}
