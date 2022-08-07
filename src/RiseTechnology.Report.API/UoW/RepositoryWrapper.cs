using RiseTechnology.Common.DependencyInjectionsLifeCycles;
using RiseTechnology.Common.GenericRepository;

namespace RiseTechnology.Report.API.UoW
{
    public class RepositoryWrapper : IRepositoryWrapper, IScopedLifetime
    {
        private readonly IUnitOfWork _unitOfWork;
        public RepositoryWrapper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IGenericRepository<Context.DbEntities.Report> ReportRepository => _unitOfWork.GetRepository<Context.DbEntities.Report>();
    }
}
