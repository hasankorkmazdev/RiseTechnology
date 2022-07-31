using AutoMapper;
using RiseTechnology.Contact.API.Context.DbEntities;
using RiseTechnology.Contact.API.Models;

namespace RiseTechnology.Contact.API.MapProfile
{
    public class ContactApiMapperProfile:Profile
    {
        public ContactApiMapperProfile()
        {
            CreateMap<CreatePerson, Person>().ReverseMap();
        }
    }
}
