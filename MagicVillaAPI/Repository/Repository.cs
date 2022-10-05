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

    public Task<List<Villa>> GetAll(Expression<Func<Villa>>? filter = null)
    {
        throw new NotImplementedException();
    }

    public Task<Villa> Get(Expression<Func<Villa>>? filter = null, bool isTracked = true)
    {
        throw new NotImplementedException();
    }

    public Task Create(Villa entity)
    {
        throw new NotImplementedException();
    }

    public Task Remove(Villa entity)
    {
        throw new NotImplementedException();
    }

    public Task Save()
    {
        throw new NotImplementedException();
    }
}
