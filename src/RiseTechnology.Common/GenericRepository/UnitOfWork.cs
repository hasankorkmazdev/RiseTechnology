using Microsoft.EntityFrameworkCore;
using RiseTechnology.Common.DbEntity.Base;
using RiseTechnology.Common.DependencyInjectionsLifeCycles;
using RiseTechnology.Common.GenericRepository;
using System;
using System.Threading.Tasks;

namespace RiseTechnology.Common.GenericRepository
{
    public class UnitOfWork : IUnitOfWork, IScopedLifetime
    {
        private readonly DbContext _context;
        private readonly IServiceProvider _serviceProvider;
        public UnitOfWork(IServiceProvider serviceProvider, DbContext context)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        IGenericRepository<TEntity> IUnitOfWork.GetRepository<TEntity>()
        {
            return _serviceProvider.GetService(typeof(IGenericRepository<TEntity>)) as IGenericRepository<TEntity>;
        }
        public async Task<int> SaveChangesAsync()
        {
            foreach (var entry in _context.ChangeTracker.Entries<EntityBaseAuditable>())
            {
                switch (entry.State)
                {
                    case Microsoft.EntityFrameworkCore.EntityState.Deleted:
                        entry.Entity.IsDeleted = true;
                        entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Added:
                        entry.Entity.IsActive = true;
                        entry.Entity.IsDeleted = false;
                        break;
                }
            }
            return await _context.SaveChangesAsync();
        }
    }
}
