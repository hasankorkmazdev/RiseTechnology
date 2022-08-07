using AutoMapper;
using RiseTechnology.Contact.API.Context.DbEntities;
using RiseTechnology.Contact.API.Models;
using RiseTechnology.Contact.API.Models.Response;

namespace RiseTechnology.Contact.API.MapProfile
{
    public class ContactApiMapperProfile:Profile
    {
        public ContactApiMapperProfile()
        {
            CreateMap<CreatePerson, Person>().ReverseMap();
            CreateMap<Person, PersonResponseModel>().ReverseMap();
            CreateMap<ContactInformation, ContactInfromationResponseModel>();
            CreateMap<Person, PersonContactInformationResponseModel>();
            CreateMap<AddPersonContactInformation, ContactInformation>();
        }
    }
}
