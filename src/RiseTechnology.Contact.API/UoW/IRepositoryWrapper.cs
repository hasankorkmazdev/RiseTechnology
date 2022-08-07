using RiseTechnology.Common.GenericRepository;
using RiseTechnology.Contact.API.Context.DbEntities;

namespace RiseTechnology.Contact.API.UoW
{
    public interface IRepositoryWrapper
    {

        public IGenericRepository<Person> PersonRepository { get; }
        public IGenericRepository<ContactInformation> ContactInformationRepository { get; }
    }
}
