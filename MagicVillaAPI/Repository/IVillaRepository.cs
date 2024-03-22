using MagicVillaAPI.Models;

namespace MagicVillaAPI.Repository;

public interface IVillaRepository
{
    Task<IEnumerable<Villa>> GetAllAsync();
    Task<Villa> GetByIdAsync(int id, bool isTracked = true);
    Task<Villa> GetByNameAsync(string name);
    Task CreateAsync(Villa entity);
    Task UpdateAsync(Villa entity);
    Task RemoveAsync(Villa entity);
    Task SaveAsync();
}