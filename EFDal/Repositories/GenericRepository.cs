using EFDal.Entities;
using EFDal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        internal readonly DbContext _dbContext;
        internal readonly DbSet<TEntity> _dbSet;

        public GenericRepository(CPDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual TEntity GetById(int id)
        {
            return _dbSet.FirstOrDefault(db => db.Id == id);
        }

        public virtual List<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public virtual int Add(TEntity entity)
        {
            entity.LastUpdate = DateTime.Now;
            _dbSet.Add(entity);
            _dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public virtual void Update(TEntity entity)
        {
            entity.LastUpdate = DateTime.Now;
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            _dbSet.Where(x => x.Id == id).ExecuteDelete();
        }

        public virtual void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            entity.LastUpdate = DateTime.Now;
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entity.Id;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(db => db.Id == id);
        }
    }
}
