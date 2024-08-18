using Announce.Application.Common.Interfaces;
using Announce.Infrastructure.Data.Contexts;
using Announce.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Announce.Infrastructure.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    private readonly ApplicationDbContext _dbContext;

    private readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbSet = dbContext.Set<TEntity>();
    }
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(TKey id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var createdEntity = await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return createdEntity.Entity;
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
        _dbContext.SaveChanges();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}

