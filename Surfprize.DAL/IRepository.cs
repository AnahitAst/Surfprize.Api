using Surfprize.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Surfprize.DAL
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        TEntity SelectOne(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> SelectOneAsync(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> SelectMany(Expression<Func<TEntity, TEntity>> expression);
        TEntity FindById(object id);
        void Delete(TEntity entity);
        void Delete(object id);
        IQueryable<TEntity> SelectAll();
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> expression);
        void Destroy(TEntity entity);
    }
}
