using Microsoft.EntityFrameworkCore;
using RiseTechnology.Common.DbEntity.Base;
using RiseTechnology.Common.GenericRepository;
using System.Linq;

namespace RiseTechnology.Common.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext _context;
        public GenericRepository(DbContext context)
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
