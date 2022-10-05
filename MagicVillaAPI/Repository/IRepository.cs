using System.Linq.Expressions;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaAPI.Repository;

public interface IRepository
{
    Task<List<Villa>> GetAll(Expression<Func<Villa>>? filter = null);
    Task<Villa> Get(Expression<Func<Villa>>? filter = null, bool isTracked = true);
    Task Create(Villa entity);
    Task Remove(Villa entity);
    Task Save();
}