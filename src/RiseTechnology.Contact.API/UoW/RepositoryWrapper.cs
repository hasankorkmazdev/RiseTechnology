using RiseTechnology.Common.DependencyInjectionsLifeCycles;
using RiseTechnology.Contact.API.Context.DbEntities;

namespace RiseTechnology.Contact.API.UoW
{
    public class RepositoryWrapper:IRepositoryWrapper,IScopedLifetime
    {
        private readonly IUnitOfWork _unitOfWork;
        public RepositoryWrapper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IGenericRepository<Person> PersonRepository=> _unitOfWork.GetRepository<Person>();
        public IGenericRepository<ContactInformation> ContactInformationRepository => _unitOfWork.GetRepository<ContactInformation>();
    }
}
