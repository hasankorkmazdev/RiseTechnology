using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RiseTechnology.Contact.API.Context.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RiseTechnology.Contact.API.Context
{
    public static class ContextSeedExtension
    {
        public static IApplicationBuilder SeedContactContext(this IApplicationBuilder app)
        {

            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ContactContext>();
            context.Database.Migrate();
            var personList = new List<Person>() {
            new Person()
            {
                UUID=Guid.Parse("9C735D5B-95B0-423C-BFE0-8BB48248A6CA"),
                Company="Rise Tech.",
                Name="Hasan",
                LastName="Korkmaz",
                 IsActive=true,
                        IsDeleted=false,
                ContactInformations=new List<ContactInformation>()
                {
                    new ContactInformation()
                    {
                        UUID=Guid.Parse("91735D5B-95B0-423C-BFE0-8BB48248A6CA"),
                        ContactType=Common.Enums.ContactType.EMail,
                        ContactContent="hasankorkmazdev@gmail.com",
                        IsActive=true,
                        IsDeleted=false,

                    },
                    new ContactInformation()
                    {
                        UUID=Guid.Parse("8C735D5B-95B0-423C-BFE0-8BB48248A6CD"),
                        ContactType=Common.Enums.ContactType.Location,
                        ContactContent="Konya",
                        IsActive=true,
                        IsDeleted=false,

                    },

                    new ContactInformation()
                    {
                        UUID=Guid.Parse("7C735D5B-95B0-423C-BFE0-8BB48248A6CD"),
                        ContactType=Common.Enums.ContactType.Telephone,
                        ContactContent="+90 555 55 55",
                        IsActive=true,
                        IsDeleted=false,

                    }
                }
            },
            new Person()
            {
                UUID=Guid.Parse("9C735D5B-95B0-423C-BFE0-8BB48248A6CB"),
                Company="Rise Tech.",
                Name="Lorem",
                LastName="Ipsum",
                 IsActive=true,
                        IsDeleted=false,
                ContactInformations=new List<ContactInformation>()
                {
                    new ContactInformation()
                    {
                        UUID=Guid.Parse("6C735D4B-95B0-423C-BFE0-8BB48248A6CC"),
                        ContactType=Common.Enums.ContactType.EMail,
                        ContactContent="loremipsum@gmail.com",
                        IsActive=true,
                        IsDeleted=false,

                    },
                    new ContactInformation()
                    {
                        UUID=Guid.Parse("5C745D5B-95A0-423C-BFE0-8BB48248A6C2"),
                        ContactType=Common.Enums.ContactType.Location,
                        ContactContent="Konya",
                        IsActive=true,
                        IsDeleted=false,

                    },

                    new ContactInformation()
                    {
                        UUID=Guid.Parse("4C735D5B-95B0-423C-DFE0-8BB48248A6C1"),
                        ContactType=Common.Enums.ContactType.Telephone,
                        ContactContent="+90 555 55 55",
                        IsActive=true,
                        IsDeleted=false,

                    }
                }
            },
            new Person()
            {
                UUID=Guid.Parse("9C735D5B-95B0-423C-BFE0-5BB48248A6CB"),
                Company="Rise Tech.",
                Name="Lorem 2",
                LastName="Ipsum 2",
                 IsActive=true,
                        IsDeleted=false,
                ContactInformations=new List<ContactInformation>()
                {
                    new ContactInformation()
                    {
                        UUID=Guid.Parse("3C735D5B-95B0-423C-BFE0-8BB48248A6CC"),
                        ContactType=Common.Enums.ContactType.EMail,
                        ContactContent="loremipsum2@gmail.com",
                        IsActive=true,
                        IsDeleted=false,

                    },
                    new ContactInformation()
                    {
                        UUID=Guid.Parse("2C735D5B-95B0-423C-BFE0-81B48248A6CA"),
                        ContactType=Common.Enums.ContactType.Location,
                        ContactContent="İstanbul",
                        IsActive=true,
                        IsDeleted=false,

                    },

                    new ContactInformation()
                    {
                        UUID=Guid.Parse("1C735D5B-95B0-423C-BFE0-82B48248A6CD"),
                        ContactType=Common.Enums.ContactType.Telephone,
                        ContactContent="+90 555 55 55",
                        IsActive=true,
                        IsDeleted=false,

                    }
                }
            }
            };

            foreach (var item in personList)
            {
                var anyItem = context.Persons.IgnoreQueryFilters().Any(x => x.UUID == item.UUID);
                if (!anyItem)
                {
                    context.Persons.Add(item);
                }
                else
                {
                    context.Persons.Update(item);
                }
            }
            context.SaveChanges();
            return app;
        }
    }

}
