using Surfprize.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Surfprize.Service.Interfaces
{
    public interface IService<TEntity> where TEntity : EntityBase
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        TEntity FindById(object id);
        void Delete(TEntity entity);
        void Delete(object id);
        void Destroy(TEntity entity);
    }
}
