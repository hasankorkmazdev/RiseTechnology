using RiseTechnology.Common.Models.Request;
using RiseTechnology.Common.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiseTechnology.Contact.API.Services
{
    public interface IContactService
    {
        public Task<PersonContactInformationResponseModel> GetPerson(Guid personUUID);
        public Task<List<PersonContactInformationResponseModel>> GetPersonList();
        public Task<bool> CreatePersonAsync(CreatePersonRequestModel model);
        public Task<bool> RemovePersonAsync(Guid personUUID);
        public Task<bool> AddPersonContactInformation(Guid personUUID, AddPersonContactInformation addPersonContactInformation);
        public Task<bool> RemoveContactInformation(Guid personUUID, Guid contactInformationUuid);


    }
}
