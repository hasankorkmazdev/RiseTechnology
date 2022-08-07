using RiseTechnology.Common.DbEntity.Base;
using System.Threading.Tasks;

namespace RiseTechnology.Common.GenericRepository
{
    public interface IUnitOfWork
    {
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;
        public Task<int> SaveChangesAsync();
    }
}
