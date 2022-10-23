using Whiskey.Data.Contracts;
using Whiskey.Data.Models;
using Whiskey.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Whiskey.Data.Repositories;
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Spiritus 
{
    protected readonly WhiskeyDb _db;

    public GenericRepository(WhiskeyDb db)
    {
        _db = db;
    }

    public async Task<TEntity> GetAsync(int id)
    {
        var entity = await _db.Set<TEntity>().FindAsync(id);
        return entity;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _db.Set<TEntity>().ToListAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _db.Set<TEntity>().AnyAsync(e => e.Id == id);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _db.AddAsync(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _db.Update(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        _db.Set<TEntity>().Remove(entity);
        return await _db.SaveChangesAsync() > 0; 
    }
}