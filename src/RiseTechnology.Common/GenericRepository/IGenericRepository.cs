using RiseTechnology.Common.DbEntity.Base;
using System.Linq;

namespace RiseTechnology.Common.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> GetQuery();
    }
}
