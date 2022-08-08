
using Microsoft.EntityFrameworkCore;
using RiseTechnology.Common.GenericRepository;
using RiseTechnology.Common.Tools.Extensions;
using RiseTechnology.Contact.API.Context.DbEntities;

namespace RiseTechnology.Contact.API.Context
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddSoftDelete();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ContactInformation> ContactInformations { get; set; }
        public DbSet<Person> Persons { get; set; }

    }

}
