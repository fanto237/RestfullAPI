using System.Linq.Expressions;
using AutoMapper;
using MagicVillaAPI.Data;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPI.Repository;

class Repository : IRepository
{
    private readonly DataContext _dbContext;


    public Repository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Villa>> GetAll(Expression<Func<Villa>>? filter = null)
    {
        return await _dbContext.Villas.ToListAsync();
    }

    public async Task<Villa> Get(Expression<Func<Villa>>? filter = null, bool isTracked = true)
    {
        var model = await _dbContext.Villas.FirstOrDefaultAsync(filter);

        return model;
    }

    public async Task Create(Villa entity)
    {
        await _dbContext.AddAsync(entity);
        await Save();
    }

    public async Task Remove(Villa entity)
    {
         _dbContext.Remove(entity);
         await Save();
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }
}
