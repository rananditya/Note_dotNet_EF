using Noted.Models;

namespace Noted.DependencyInjections.Interfaces
{
    public interface INotesServices
    {
        Task<List<Notes>> GetAllAsync();

        Task<Notes?> GetByIdAsync(Guid id);

        Task CreateAsync(Notes note);

        Task UpdateAsync(Notes note);

        Task DeleteAsync(Guid id);
    }
}
