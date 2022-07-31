
using Microsoft.EntityFrameworkCore;
using RiseTechnology.Contact.API.Context.DbEntities;

namespace RiseTechnology.Contact.API.Context
{
    public class ContactContext :DbContext
    {
        public ContactContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<ContactInformation> ContactInformations { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}
