using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RiseTechnology.Common.DependencyInjectionsLifeCycles;
using RiseTechnology.Common.GenericRepository;
using RiseTechnology.Common.Models.Base;
using RiseTechnology.Common.Models.Request;
using RiseTechnology.Common.Models.Response;
using RiseTechnology.Common.Tools.Exceptions;
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

        public async Task<ServiceResultModel> GetPerson(Guid personUUID)
        {
            if (!await _repositoryWrapper.PersonRepository.GetQuery().AnyAsync(x => x.UUID == personUUID))
                throw new BadRequestException($"personUuid:{personUUID} not found please check personUuid");

            var personEntity = await _repositoryWrapper.PersonRepository.GetQuery().Where(x => x.UUID == personUUID).Include(x => x.ContactInformations).FirstOrDefaultAsync();
            var mappedData = _mapper.Map<PersonContactInformationResponseModel>(personEntity);
            return new ServiceResultModel(mappedData);
        }

        public async Task<ServiceResultModel> GetPersonList()
        {
            var personEntityList = await _repositoryWrapper.PersonRepository.GetQuery().Include(x=> x.ContactInformations).ToListAsync();
            var mappedList = _mapper.Map<List<PersonContactInformationResponseModel>>(personEntityList);
            return new ServiceResultModel(mappedList);

        }

        public async Task<ServiceResultModel> CreatePersonAsync(CreatePersonRequestModel model)
        {
            var mappedEntity = _mapper.Map<Person>(model);
            _repositoryWrapper.PersonRepository.Add(mappedEntity);
            await _unitOfWork.SaveChangesAsync();
            return new ServiceResultModel(mappedEntity);
        }

        public async Task<ServiceResultModel> RemovePersonAsync(Guid personUUID)
        {
            if (!await _repositoryWrapper.PersonRepository.GetQuery().AnyAsync(x => x.UUID == personUUID))
                throw new BadRequestException($"personUuid:{personUUID} not found please check personUuid");

            var person = await _repositoryWrapper.PersonRepository.GetQuery().Where(x => x.UUID == personUUID).FirstOrDefaultAsync();
            _repositoryWrapper.PersonRepository.Delete(person);
            await _unitOfWork.SaveChangesAsync(); 
            return new ServiceResultModel($"personUuid:{personUUID}");

        }

        public async Task<ServiceResultModel> AddPersonContactInformation(Guid personUUID, AddPersonContactInformation addPersonContactInformation)
        {
            if (!await _repositoryWrapper.PersonRepository.GetQuery().AnyAsync(x => x.UUID == personUUID))
                throw new BadRequestException($"personUuid:{personUUID} not found please check personUuid");

            var mappedEntity = _mapper.Map<ContactInformation>(addPersonContactInformation);
            mappedEntity.PersonUUID = personUUID;
            _repositoryWrapper.ContactInformationRepository.Add(mappedEntity);
            await _unitOfWork.SaveChangesAsync();
            return new ServiceResultModel(mappedEntity);
        }

        public async Task<ServiceResultModel> RemoveContactInformation(Guid personUUID, Guid contactInformationUuid)
        {
          
            if (!(await _repositoryWrapper.PersonRepository.GetQuery().AnyAsync(x => x.UUID == personUUID && x.ContactInformations.Any(y => y.UUID == contactInformationUuid))))
                throw new BadRequestException($"personUuid:{personUUID} or contactInformationUuid:${contactInformationUuid} not found please check personUuid and contactInformationUuid");

            _repositoryWrapper.ContactInformationRepository.Delete(_repositoryWrapper.ContactInformationRepository.GetQuery().Where(x => x.PersonUUID == personUUID && x.UUID == contactInformationUuid).FirstOrDefault());
            await _unitOfWork.SaveChangesAsync();
            return new ServiceResultModel($"personUuid:{personUUID} contactInformationUuid:${contactInformationUuid}");
        }


    }
}
