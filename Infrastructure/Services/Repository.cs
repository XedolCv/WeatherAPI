using System.Linq.Expressions;
using Domain.Entitis;
using Infractructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infractructure.Services;

public class Repository<TCon> : IRepository<TCon>
    where TCon : DbContext
{
    private readonly TCon _context;

    public Repository(TCon context)
    {
        _context = context;
        Context = context;
    }

    public TCon Context { get; set; }

    public IQueryable<T> Get<T>(Expression<Func<T, bool>> selector) where T : BaseEntity
    {
        return _context.Set<T>().Where(selector).AsQueryable();
    }

    public async Task<T?> GetLastAsync<T>() where T : BaseEntity
    {
        return await _context.Set<T>().OrderBy(e=>e.CreateTime).AsQueryable().LastOrDefaultAsync();
    }

    public async Task AddAsync<T>(T entity) where T : BaseEntity =>
        await _context.Set<T>().AddAsync(entity);

    public async Task AddRangeAsync<T>(params T[] entity) where T : BaseEntity
    {
        await _context.Set<T>().AddRangeAsync(entity);
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}