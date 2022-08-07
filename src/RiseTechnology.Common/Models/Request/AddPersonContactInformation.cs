using static RiseTechnology.Common.Enums;

namespace RiseTechnology.Common.Models.Request
{
    public class AddPersonContactInformation
    {
        public ContactType ContactType { get; set; }
        public string ContactContent { get; set; }
    }
}
