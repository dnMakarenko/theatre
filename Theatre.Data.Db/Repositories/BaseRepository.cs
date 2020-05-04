using Theatre.Data.Core.Models;
using Theatre.Data.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Theatre.Data.Db.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _dbContext;

        public DbSet<T> DbSet { get; set; }

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            DbSet = _dbContext.Set<T>();
        }

        #region Sync Operations
        public IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T GetById(Guid Id)
        {
            return DbSet.Find(Id);
        }

        public T Create(T entity)
        {
            var e = DbSet.Add(entity);

            Save();

            return e.Entity;
        }

        public T Update(T entity)
        {
            var e = DbSet.Update(entity);

            Save();

            return e.Entity;
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);

            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        #endregion

        #region Async Operations
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid Id)
        {
            return await DbSet.FindAsync(Id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            var e = DbSet.Add(entity);

            await SaveAsync();

            return e.Entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var e = DbSet.Update(entity);

            await SaveAsync();

            return e.Entity;
        }

        public async Task DeleteAsync(T entity)
        {
            DbSet.Remove(entity);

            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}
