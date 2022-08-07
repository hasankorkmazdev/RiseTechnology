using AutoMapper;
using RiseTechnology.Common.Models.Request;
using RiseTechnology.Common.Models.Response;
using RiseTechnology.Contact.API.Context.DbEntities;

namespace RiseTechnology.Contact.API.MapProfile
{
    public class ContactApiMapperProfile : Profile
    {
        public ContactApiMapperProfile()
        {
            CreateMap<CreatePersonRequestModel, Person>().ReverseMap();
            CreateMap<ContactInformation, ContactInfromationResponseModel>();
            CreateMap<Person, PersonContactInformationResponseModel>();
            CreateMap<AddPersonContactInformation, ContactInformation>();
        }
    }
}
