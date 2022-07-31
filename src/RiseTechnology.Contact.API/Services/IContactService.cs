using RiseTechnology.Contact.API.Models;
using System.Threading.Tasks;

namespace RiseTechnology.Contact.API.Services
{
    public interface IContactService
    {
        public Task<bool> CreatePersonAsync(CreatePerson model);

    }
}
