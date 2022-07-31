using RiseTechnology.Common.DbEntity.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RiseTechnology.Contact.API.UoW
{
    public interface IGenericRepository<TEntity> where TEntity:class, IEntity
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IQueryable GetQuery(TEntity entity);
    }
}
