using System;
using static RiseTechnology.Common.Enums;

namespace RiseTechnology.Contact.API.Models
{
    public class AddPersonContactInformation
    {
        public ContactType ContactType { get; set; }
        public string ContactContent { get; set; }
    }
}
