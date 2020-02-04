using Microsoft.EntityFrameworkCore;
using Surfprize.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Surfprize.DAL
{
    public class Repository<TEntity> : IRepository<TEntity>
         where TEntity : EntityBase
    {
        private readonly SurfprizeDbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public Repository(SurfprizeDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            DateTime now = DateTime.UtcNow;

            entity.CreatedDate = now;
            entity.UpdateDate = now;
            dbSet.Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            DateTime now = DateTime.UtcNow;

            entity.CreatedDate = now;
            entity.UpdateDate = now;
            await dbSet.AddAsync(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            DateTime now = DateTime.UtcNow;

            foreach (TEntity entity in entities)
            {
                entity.CreatedDate = now;
                entity.UpdateDate = now;
            }

            dbSet.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            dbSet.Update(entity);
        }

        public TEntity SelectOne(Expression<Func<TEntity, bool>> expression) => dbSet.Single(expression);

        public async Task<TEntity> SelectOneAsync(Expression<Func<TEntity, bool>> expression) => await dbSet.SingleAsync(expression);

        public IQueryable<TEntity> SelectMany(Expression<Func<TEntity, TEntity>> expression) => dbSet.Select(expression);

        public TEntity FindById(object id) => dbSet.Find(id);

        public void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public void Delete(object id)
        {
            TEntity entity = FindById(id);
            if (entity == null)
                throw new ArgumentException("Incorrect id!");

            entity.IsDeleted = true;
            Update(entity);
        }

        public IQueryable<TEntity> SelectAll() => dbSet.AsQueryable<TEntity>();

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> expression) => dbSet.Where(expression);

        public void Destroy(TEntity entity) => dbSet.Remove(entity);
    }
}
