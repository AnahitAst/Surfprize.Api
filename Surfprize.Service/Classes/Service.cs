using Surfprize.Entity;
using Surfprize.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Surfprize.DAL;

namespace Surfprize.Service.Classes
{
    public class Service<TEntity> : IService<TEntity>
        where TEntity : EntityBase
    {
        protected IRepository<TEntity> Repository { get; private set; }

        public Service(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public void Add(TEntity entity) => Repository.Add(entity);

        public Task AddAsync(TEntity entity) => Repository.AddAsync(entity);

        public void Update(TEntity entity) => Repository.Update(entity);

        public TEntity FindById(object id) => Repository.FindById(id);

        public void Delete(TEntity entity) => Repository.Delete(entity);

        public void Delete(object id) => Repository.Delete(id);

        public void Destroy(TEntity entity) => Repository.Destroy(entity);

        public void AddRange(IEnumerable<TEntity> entities) => Repository.AddRange(entities);
    }
}
