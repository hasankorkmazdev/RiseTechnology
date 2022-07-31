using AutoMapper;
using RiseTechnology.Common.DependencyInjectionsLifeCycles;
using RiseTechnology.Contact.API.Context;
using RiseTechnology.Contact.API.Context.DbEntities;
using RiseTechnology.Contact.API.Models;
using RiseTechnology.Contact.API.UoW;
using System.Threading.Tasks;

namespace RiseTechnology.Contact.API.Services
{
    public class ContactService : IContactService, ITransientLifetime
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ContactService(IRepositoryWrapper repositoryWrapper,IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> CreatePersonAsync(CreatePerson model)
        {
            var mappedEntity = _mapper.Map<Person>(model);
            _repositoryWrapper.PersonRepository.Add(mappedEntity);
           await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
