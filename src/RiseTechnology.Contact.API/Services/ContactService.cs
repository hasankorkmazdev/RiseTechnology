using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RiseTechnology.Common.DependencyInjectionsLifeCycles;
using RiseTechnology.Common.GenericRepository;
using RiseTechnology.Common.Models.Request;
using RiseTechnology.Common.Models.Response;
using RiseTechnology.Contact.API.Context.DbEntities;
using RiseTechnology.Contact.API.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiseTechnology.Contact.API.Services
{
    public class ContactService : IContactService, ITransientLifetime
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ContactService(IRepositoryWrapper repositoryWrapper, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PersonContactInformationResponseModel> GetPerson(Guid personUUID)
        {

            if (personUUID == Guid.Empty || !await _repositoryWrapper.PersonRepository.GetQuery().AnyAsync(x => x.UUID == personUUID))
            {
                return null;
            }
            var personEntity = await _repositoryWrapper.PersonRepository.GetQuery().Where(x => x.UUID == personUUID).Include(x => x.ContactInformations).FirstOrDefaultAsync();
            var mappedData = _mapper.Map<PersonContactInformationResponseModel>(personEntity);
            return mappedData;
        }

        public async Task<List<PersonContactInformationResponseModel>> GetPersonList()
        {
            var personEntityList = await _repositoryWrapper.PersonRepository.GetQuery().ToListAsync();
            var mappedList = _mapper.Map<List<PersonContactInformationResponseModel>>(personEntityList);
            return mappedList;
        }

        public async Task<bool> CreatePersonAsync(CreatePersonRequestModel model)
        {
            var mappedEntity = _mapper.Map<Person>(model);
            _repositoryWrapper.PersonRepository.Add(mappedEntity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemovePersonAsync(Guid personUUID)
        {
            var person = _repositoryWrapper.PersonRepository.GetQuery().Where(x => x.UUID == personUUID).FirstOrDefault();
            if (person == null)
            {
                return false;
            }
            _repositoryWrapper.PersonRepository.Delete(person);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddPersonContactInformation(Guid personUUID, AddPersonContactInformation addPersonContactInformation)
        {
            if (personUUID == Guid.Empty || !_repositoryWrapper.PersonRepository.GetQuery().Any(x => x.UUID == personUUID))
            {
                return false;
            }
            var mappedEntity = _mapper.Map<ContactInformation>(addPersonContactInformation);
            mappedEntity.PersonUUID = personUUID;
            _repositoryWrapper.ContactInformationRepository.Add(mappedEntity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveContactInformation(Guid personUUID, Guid contactInformationUuid)
        {
            if (personUUID == Guid.Empty || contactInformationUuid == Guid.Empty || !(await _repositoryWrapper.PersonRepository.GetQuery().AnyAsync(x => x.UUID == personUUID && x.ContactInformations.Any(y => y.UUID == contactInformationUuid))))
            {
                return false;
            }

            _repositoryWrapper.ContactInformationRepository.Delete(_repositoryWrapper.ContactInformationRepository.GetQuery().Where(x => x.PersonUUID == personUUID && x.UUID == contactInformationUuid).FirstOrDefault());
            await _unitOfWork.SaveChangesAsync();
            return true;
        }


    }
}
