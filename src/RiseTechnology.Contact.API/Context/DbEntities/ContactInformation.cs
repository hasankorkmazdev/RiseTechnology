using RiseTechnology.Common.DbEntity.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static RiseTechnology.Common.Enums;

namespace RiseTechnology.Contact.API.Context.DbEntities
{
    [Table(name: "ContactInformations", Schema = "RiseContact")]
    public class ContactInformation : EntityBaseAuditable
    {
        public ContactType ContactType { get; set; }
        public string ContactContent { get; set; }
        public Guid PersonUUID { get; set; }
        public virtual Person Person { get; set; }

    }
}
