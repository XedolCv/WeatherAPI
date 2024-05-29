using System.Linq.Expressions;
using Domain.Entitis;
using Microsoft.EntityFrameworkCore;

namespace Infractructure.Interfaces;

public interface IRepository<TContext> where TContext : DbContext
{
    public TContext Context { get; set; }
    IQueryable<TContext> Get<TContext>(Expression<Func<TContext, bool>> selector) where TContext : BaseEntity;
    Task<TContext?> GetLastAsync<TContext>() where TContext : BaseEntity;
    Task AddAsync<TContext>(TContext entity) where TContext : BaseEntity;
    Task AddRangeAsync<TContext>(params TContext[] entity) where TContext : BaseEntity;
    Task<int> SaveChangesAsync();
}