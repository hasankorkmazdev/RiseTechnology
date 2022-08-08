using RiseTechnology.Common.Models.Base;
using RiseTechnology.Common.Models.Request;
using RiseTechnology.Common.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiseTechnology.Contact.API.Services
{
    public interface IContactService
    {
        public Task<ServiceResultModel> GetPerson(Guid personUUID);
        public Task<ServiceResultModel> GetPersonList();
        public Task<ServiceResultModel> CreatePersonAsync(CreatePersonRequestModel model);
        public Task<ServiceResultModel> RemovePersonAsync(Guid personUUID);
        public Task<ServiceResultModel> AddPersonContactInformation(Guid personUUID, AddPersonContactInformation addPersonContactInformation);
        public Task<ServiceResultModel> RemoveContactInformation(Guid personUUID, Guid contactInformationUuid);


    }
}
