using RiseTechnology.Common.DbEntity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiseTechnology.Contact.API.Context.DbEntities
{
    [Table(name: "Persons", Schema = "RiseContact")]
    public class Person : EntityBaseAuditable
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
    }
}
