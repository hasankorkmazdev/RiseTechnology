using RiseTechnology.Common.DbEntity.Base;
using RiseTechnology.Contact.API.Context;
using System.Linq;

namespace RiseTechnology.Contact.API.UoW
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly ContactContext _context;
        public GenericRepository(ContactContext context)
        {
            _context = context;
        }
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

       
        public IQueryable<TEntity> GetQuery()
        {
            return _context.Set<TEntity>().AsQueryable();

        }


    }
}
