using System;
using AuctionService.Data;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Repositories.Generic;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AuctionDBContext _auctionDBContext;

    public GenericRepository(AuctionDBContext auctionDBContext)
    {
        _auctionDBContext = auctionDBContext;
    }
    public void Add(T entity)
    {
        _auctionDBContext.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        _auctionDBContext.Set<T>().Remove(entity);

    }

    public void Update(T entity)
    {
        _auctionDBContext.Set<T>().Update(entity);

    }


    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _auctionDBContext.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _auctionDBContext.Set<T>().FindAsync(id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _auctionDBContext.SaveChangesAsync() > 0;
    }
}

