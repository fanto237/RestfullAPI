using MagicVillaAPI.Data;
using MagicVillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPI.Repository;

internal class VillaRepository : IVillaRepository
{
    private readonly DataContext _dbContext;

    public VillaRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Villa>> GetAllAsync()
    {
        return await _dbContext.Villas.ToListAsync();
    }

    public async Task<Villa> GetByIdAsync(int id, bool isTracked = true)
    {
        if (!isTracked)
        {
            var result = await _dbContext.Villas.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);
            return result;
        }

        var res = await _dbContext.Villas.FirstOrDefaultAsync(item => item.Id == id);
        return res;
    }

    public async Task<Villa> GetByNameAsync(string name)
    {
        var result = await _dbContext.Villas.FirstOrDefaultAsync(item => item.Name == name);
        return result;
    }

    public async Task CreateAsync(Villa entity)
    {
        await _dbContext.Villas.AddAsync(entity);
        await SaveAsync();
    }

    public async Task UpdateAsync(Villa entity)
    {
        _dbContext.Villas.Update(entity);
        await SaveAsync();
    }

    public async Task RemoveAsync(Villa entity)
    {
        _dbContext.Villas.Remove(entity);
        await SaveAsync();
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}